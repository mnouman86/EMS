import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TooltipModule } from 'primeng/tooltip';
import { ConfirmationService } from 'primeng/api';
import { ParentService } from '../../parent/parent.service';
import { ParentChild } from '../../parent/parent.models';
import { PermissionService } from '../../core/services/permission.service';
import { PermissionUser } from '../../core/models/permission.models';
import { StudentService } from '../../students/student.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';
import { Roles } from '../../core/models/roles';

@Component({
  selector: 'app-parent-links',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, SelectModule, ButtonModule, InputTextModule, ConfirmDialogModule, TooltipModule],
  providers: [ConfirmationService],
  templateUrl: './parent-links.html'
})
export class ParentLinks implements OnInit {
  private parent = inject(ParentService);
  private perms = inject(PermissionService);
  private studentSvc = inject(StudentService);
  private toast = inject(ToastService);
  private confirm = inject(ConfirmationService);

  parentOptions = signal<{ label: string; value: number }[]>([]);
  studentOptions = signal<{ label: string; value: number }[]>([]);
  parentUserId: number | null = null;

  children = signal<ParentChild[]>([]);
  loadingChildren = signal(false);

  // link form
  newStudentId: number | null = null;
  relationship = 'Parent';
  linking = signal(false);

  relationships = [
    { label: 'Father', value: 'Father' },
    { label: 'Mother', value: 'Mother' },
    { label: 'Guardian', value: 'Guardian' },
    { label: 'Parent', value: 'Parent' }
  ];

  ngOnInit(): void {
    // Resolve the parent role id, then load parent users.
    this.perms.getRoles().subscribe(res => {
      const parentRole = (res.data ?? []).find(r => r.roleName?.toLowerCase() === Roles.Parent);
      this.perms.getUsers(null, parentRole?.roleId ?? null, true, 1, 500).subscribe(u => {
        this.parentOptions.set((u.data ?? []).map((x: PermissionUser) => ({
          label: `${x.fullName || x.userName} (${x.email || x.userName})`, value: x.userId
        })));
      });
    });

    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      this.studentOptions.set((res.data ?? []).map(s => ({
        label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`, value: s.id
      })));
    });
  }

  onParentChange(): void {
    if (!this.parentUserId) { this.children.set([]); return; }
    this.loadingChildren.set(true);
    this.parent.getChildrenOf(this.parentUserId).subscribe({
      next: res => { this.children.set(res.data ?? []); this.loadingChildren.set(false); },
      error: () => this.loadingChildren.set(false)
    });
  }

  link(): void {
    if (!this.parentUserId) { this.toast.warn('Select a parent.'); return; }
    if (!this.newStudentId) { this.toast.warn('Select a student.'); return; }
    if (this.children().some(c => c.studentId === this.newStudentId)) {
      this.toast.warn('This student is already linked to the parent.'); return;
    }
    this.linking.set(true);
    this.parent.linkChild(this.parentUserId, this.newStudentId, this.relationship).subscribe({
      next: res => {
        this.linking.set(false);
        if (res.isSuccess) {
          this.toast.success('Child linked.');
          this.newStudentId = null;
          this.onParentChange();
        } else {
          this.toast.error(res.message || 'Could not link the child.');
        }
      },
      error: () => { this.linking.set(false); this.toast.error('Could not link the child.'); }
    });
  }

  unlink(child: ParentChild): void {
    if (!this.parentUserId) return;
    this.confirm.confirm({
      message: `Remove ${child.fullName} from this parent's account?`,
      header: 'Confirm unlink',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.parent.unlinkChild(this.parentUserId!, child.studentId).subscribe({
          next: res => {
            if (res.isSuccess) { this.toast.success('Child unlinked.'); this.onParentChange(); }
            else this.toast.error(res.message || 'Could not unlink the child.');
          },
          error: () => this.toast.error('Could not unlink the child.')
        });
      }
    });
  }
}
