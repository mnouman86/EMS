import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { TagModule } from 'primeng/tag';
import { AuthService } from '../../core/services/auth.service';
import { ParentService } from '../parent.service';

@Component({
  selector: 'app-parent-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, TagModule],
  templateUrl: './parent-dashboard.html'
})
export class ParentDashboard implements OnInit {
  private auth = inject(AuthService);
  parent = inject(ParentService);

  user = this.auth.currentUser;
  loading = signal(false);

  ngOnInit(): void {
    this.loading.set(true);
    this.parent.loadChildren().subscribe({
      next: () => this.loading.set(false),
      error: () => this.loading.set(false)
    });
  }

  select(studentId: number): void {
    this.parent.selectChild(studentId);
  }

  statusSeverity(status?: string | null): 'success' | 'info' | 'warn' | 'secondary' {
    switch ((status || '').toLowerCase()) {
      case 'active': return 'success';
      case 'admitted': return 'info';
      case 'alumni': return 'secondary';
      default: return 'warn';
    }
  }
}
