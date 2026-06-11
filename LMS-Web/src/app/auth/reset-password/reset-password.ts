import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

function passwordsMatch(group: AbstractControl): ValidationErrors | null {
  const pwd = group.get('newPassword')?.value;
  const confirm = group.get('confirmNewPassword')?.value;
  return pwd && confirm && pwd !== confirm ? { mismatch: true } : null;
}

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, PasswordModule, ButtonModule, MessageModule],
  templateUrl: './reset-password.html'
})
export class ResetPassword {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(ToastService);

  loading = signal(false);
  errorMsg = signal<string | null>(null);

  // Reset link arrives as /reset-password?email=...&token=...
  private email = this.route.snapshot.queryParamMap.get('email') ?? '';
  private token = this.route.snapshot.queryParamMap.get('token') ?? '';
  readonly linkValid = !!this.email && !!this.token;

  form = this.fb.nonNullable.group(
    {
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmNewPassword: ['', [Validators.required]]
    },
    { validators: passwordsMatch }
  );

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.errorMsg.set(null);
    this.loading.set(true);
    const { newPassword, confirmNewPassword } = this.form.getRawValue();
    this.auth.resetPassword(this.email, this.token, newPassword, confirmNewPassword).subscribe({
      next: msg => {
        this.loading.set(false);
        this.toast.success(msg);
        this.router.navigate(['/login']);
      },
      error: err => {
        this.loading.set(false);
        this.errorMsg.set(err?.error?.Message || err?.message || 'Reset failed.');
      }
    });
  }
}
