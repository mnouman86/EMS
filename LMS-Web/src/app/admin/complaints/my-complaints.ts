import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { DialogModule } from 'primeng/dialog';
import { ComplaintService, ComplaintRow, ComplaintDetail } from '../../core/services/complaint.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-my-complaints',
  standalone: true,
  imports: [CommonModule, RouterLink, TableModule, ButtonModule, TagModule, TooltipModule, DialogModule],
  templateUrl: './my-complaints.html'
})
export class MyComplaints implements OnInit {
  private svc = inject(ComplaintService);
  private toast = inject(ToastService);
  private router = inject(Router);

  rows = signal<ComplaintRow[]>([]);
  loading = signal(false);

  detailOpen = signal(false);
  detail = signal<ComplaintDetail | null>(null);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.svc.getMine().subscribe({
      next: r => { this.rows.set(r.data ?? []); this.loading.set(false); },
      error: () => { this.toast.error('Failed to load your complaints.'); this.loading.set(false); }
    });
  }

  view(r: ComplaintRow): void {
    this.svc.getDetail(r.complaintId).subscribe({
      next: res => { this.detail.set(res.data ?? null); this.detailOpen.set(true); },
      error: () => this.toast.error('Failed to load complaint detail.')
    });
  }

  edit(r: ComplaintRow): void { this.router.navigate(['/admin/complaints', r.complaintId, 'edit']); }

  statusSeverity(s: string): 'info' | 'warn' | 'success' | 'danger' | 'secondary' {
    switch (s) {
      case 'New': return 'info';
      case 'InReview': return 'warn';
      case 'Resolved': return 'success';
      case 'Closed': return 'secondary';
      case 'Rejected': return 'danger';
      default: return 'secondary';
    }
  }

  attUrl(r: ComplaintRow): string | null {
    return r.attachmentPath ? this.svc.attachmentUrl(r.attachmentPath, r.attachmentOriginalName) : null;
  }
}
