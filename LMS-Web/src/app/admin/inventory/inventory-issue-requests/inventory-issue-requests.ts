import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { TextareaModule } from 'primeng/textarea';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Roles } from '../../../core/models/roles';
import { InventoryService } from '../inventory.service';
import {
  IssueRequestRow, IssueRequestLineRow, IssueRequestLineInput, InventoryItem
} from '../inventory.models';
import { defaultSearch } from '../../../core/models/search-request';

@Component({
  selector: 'app-inventory-issue-requests',
  standalone: true,
  imports: [
    CommonModule, FormsModule, RouterLink,
    TableModule, ButtonModule, SelectModule, DatePickerModule, InputTextModule, InputNumberModule,
    TagModule, DialogModule, TextareaModule, ConfirmDialogModule
  ],
  providers: [ConfirmationService],
  templateUrl: './inventory-issue-requests.html'
})
export class InventoryIssueRequests implements OnInit {
  private svc = inject(InventoryService);
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  /* Role flags */
  readonly canFulfil = computed(() =>
    this.auth.hasAnyRole([Roles.Admin, Roles.Principal, Roles.Accountant]));

  /* Filters */
  statusOptions = [
    { label: 'All', value: null },
    { label: 'Pending', value: 'Pending' },
    { label: 'Approved', value: 'Approved' },
    { label: 'Rejected', value: 'Rejected' },
    { label: 'Fulfilled', value: 'Fulfilled' }
  ];
  statusFilter: string | null = 'Pending';
  range: Date[] = [];

  rows = signal<IssueRequestRow[]>([]);
  loading = signal(false);

  /* Drill-down */
  selected = signal<IssueRequestRow | null>(null);
  selectedLines = signal<IssueRequestLineRow[]>([]);
  detailVisible = signal(false);

  /* New-request modal */
  createOpen = signal(false);
  newPurpose = '';
  newLines: { itemId: number | null; quantity: number | null }[] = [{ itemId: null, quantity: 1 }];
  itemOptions = signal<{ label: string; value: number; stock: number }[]>([]);
  saving = signal(false);

  /* Reject modal */
  rejectOpen = signal(false);
  rejectingId: number | null = null;
  rejectReason = '';

  ngOnInit(): void {
    const now = new Date();
    this.range = [new Date(now.getFullYear(), 0, 1), now];

    this.svc.getItems(defaultSearch()).subscribe(res => {
      this.itemOptions.set((res.data ?? []).map((i: InventoryItem) => ({
        label: `${i.name}${i.code ? ' (' + i.code + ')' : ''} — stock ${i.currentStock}`,
        value: i.id,
        stock: i.currentStock
      })));
    });

    this.load();
  }

  load(): void {
    const from = this.range?.[0]?.toISOString() ?? null;
    const to   = this.range?.[1]?.toISOString() ?? null;
    this.loading.set(true);
    // mineOnly is enforced server-side for non-finance callers — sending false is safe.
    this.svc.getIssueRequests(false, this.statusFilter, from, to).subscribe({
      next: res => { this.rows.set(res.data ?? []); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  statusSeverity(s: string): 'info' | 'success' | 'warn' | 'danger' | 'secondary' {
    switch (s) {
      case 'Pending':   return 'warn';
      case 'Approved':  return 'info';
      case 'Fulfilled': return 'success';
      case 'Rejected':  return 'danger';
      default:          return 'secondary';
    }
  }

  /* --- Drill into a request --- */
  open(row: IssueRequestRow): void {
    this.selected.set(row);
    this.detailVisible.set(true);
    this.selectedLines.set([]);
    this.svc.getIssueRequestLines(row.id).subscribe(res => this.selectedLines.set(res.data ?? []));
  }

  /* --- Create a new request (any user) --- */
  openCreate(): void {
    this.newPurpose = '';
    this.newLines = [{ itemId: null, quantity: 1 }];
    this.createOpen.set(true);
  }
  addLine(): void { this.newLines.push({ itemId: null, quantity: 1 }); }
  removeLine(i: number): void { this.newLines.splice(i, 1); if (!this.newLines.length) this.addLine(); }

  submitCreate(): void {
    if (!this.newPurpose.trim()) { this.toast.warn('Purpose is required.'); return; }
    const lines: IssueRequestLineInput[] = this.newLines
      .filter(l => l.itemId && (l.quantity ?? 0) > 0)
      .map(l => ({ itemId: l.itemId!, quantity: l.quantity! }));
    if (!lines.length) { this.toast.warn('At least one line with item + quantity is required.'); return; }

    this.saving.set(true);
    this.svc.createIssueRequest({ purpose: this.newPurpose.trim(), lines }).subscribe({
      next: res => {
        this.saving.set(false);
        if (res.isSuccess) {
          this.toast.success(res.message || 'Request submitted.');
          this.createOpen.set(false);
          this.load();
        } else this.toast.error(res.message || 'Could not submit request.');
      },
      error: () => { this.saving.set(false); this.toast.error('Could not submit request.'); }
    });
  }

  /* --- Approve / Reject / Fulfill (finance roles only) --- */
  approve(row: IssueRequestRow): void {
    this.confirm.confirm({
      header: 'Approve request',
      message: `Approve ${row.requestCode}? Stock won't be deducted yet — click "Fulfill" afterwards to issue.`,
      icon: 'pi pi-check',
      accept: () => {
        this.svc.approveIssueRequest(row.id).subscribe({
          next: res => {
            if (res.isSuccess) { this.toast.success(res.message || 'Approved.'); this.load(); }
            else this.toast.error(res.message || 'Could not approve.');
          },
          error: () => this.toast.error('Could not approve.')
        });
      }
    });
  }

  openReject(row: IssueRequestRow): void {
    this.rejectingId = row.id;
    this.rejectReason = '';
    this.rejectOpen.set(true);
  }
  submitReject(): void {
    if (!this.rejectingId) return;
    if (!this.rejectReason.trim()) { this.toast.warn('Reason is required.'); return; }
    this.svc.rejectIssueRequest(this.rejectingId, this.rejectReason.trim()).subscribe({
      next: res => {
        if (res.isSuccess) {
          this.toast.success(res.message || 'Rejected.');
          this.rejectOpen.set(false); this.rejectingId = null; this.load();
        } else this.toast.error(res.message || 'Could not reject.');
      },
      error: () => this.toast.error('Could not reject.')
    });
  }

  fulfill(row: IssueRequestRow): void {
    this.confirm.confirm({
      header: 'Fulfill request',
      message: `Issue stock for ${row.requestCode} now? An InventoryIssue will be created and item stock deducted.`,
      icon: 'pi pi-truck',
      accept: () => {
        this.svc.fulfillIssueRequest(row.id, null).subscribe({
          next: res => {
            if (res.isSuccess) { this.toast.success(res.message || 'Fulfilled.'); this.load(); }
            else this.toast.error(res.message || 'Could not fulfill.');
          },
          error: () => this.toast.error('Could not fulfill.')
        });
      }
    });
  }
}
