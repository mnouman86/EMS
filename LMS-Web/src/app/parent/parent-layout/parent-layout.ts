import { Component, computed, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive, RouterOutlet, Router } from '@angular/router';
import { SelectModule } from 'primeng/select';
import { ToastModule } from 'primeng/toast';
import { AuthService } from '../../core/services/auth.service';
import { ParentService } from '../parent.service';

interface NavItem { label: string; icon: string; route: string; }

@Component({
  selector: 'app-parent-layout',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink, RouterLinkActive, SelectModule, ToastModule],
  templateUrl: './parent-layout.html'
})
export class ParentLayout implements OnInit {
  private auth = inject(AuthService);
  private router = inject(Router);
  parent = inject(ParentService);

  collapsed = signal(false);
  user = this.auth.currentUser;

  readonly nav: NavItem[] = [
    { label: 'Overview', icon: 'pi pi-home', route: '/parent/dashboard' },
    { label: 'Profile', icon: 'pi pi-user', route: '/parent/profile' },
    { label: 'Fees', icon: 'pi pi-wallet', route: '/parent/fees' },
    { label: 'Attendance', icon: 'pi pi-calendar-times', route: '/parent/attendance' },
    { label: 'Results', icon: 'pi pi-chart-line', route: '/parent/results' }
  ];

  childOptions = computed(() =>
    this.parent.children().map(c => ({
      label: `${c.fullName}${c.className ? ' · ' + c.className : ''}`,
      value: c.studentId
    }))
  );

  selectedId = computed(() => this.parent.selectedChildId());

  ngOnInit(): void {
    this.parent.loadChildren().subscribe();
  }

  onSelect(id: number): void {
    this.parent.selectChild(id);
  }

  toggle(): void { this.collapsed.update(v => !v); }

  logout(): void {
    this.parent.clear();
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
