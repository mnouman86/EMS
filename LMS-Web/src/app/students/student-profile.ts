import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FieldsetModule } from 'primeng/fieldset';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { StudentService } from './student.service';
import { StudentDetail } from './student.models';

@Component({
  selector: 'app-student-profile',
  standalone: true,
  imports: [CommonModule, DatePipe, RouterLink, FieldsetModule, ButtonModule, TagModule],
  templateUrl: './student-profile.html'
})
export class StudentProfile implements OnInit {
  private svc = inject(StudentService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  id!: number;
  loading = signal(false);
  student = signal<StudentDetail | null>(null);

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.svc.getById(this.id).subscribe({
      next: res => { this.student.set(res.data ?? null); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  statusSeverity(status?: string | null): 'success' | 'info' | 'warn' | 'secondary' | 'danger' {
    switch (status) {
      case 'Active': return 'success';
      case 'Admitted': return 'info';
      case 'Applied': return 'warn';
      case 'Withdrawn': return 'danger';
      default: return 'secondary';
    }
  }

  edit(): void { this.router.navigate(['/admin/students', this.id, 'edit']); }
}
