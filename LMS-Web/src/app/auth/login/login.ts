import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink,
    InputTextModule, PasswordModule, CheckboxModule, ButtonModule, MessageModule
  ],
  templateUrl: './login.html'
})
export class Login implements OnInit {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private toast = inject(ToastService);

  loading = signal(false);
  errorMsg = signal<string | null>(null);

  ngOnInit(): void {
    // Already signed in → skip the login screen.
    if (this.auth.isAuthenticated()) {
      this.router.navigateByUrl(this.auth.landingRoute());
    }
  }

  form = this.fb.nonNullable.group({
    userName: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    rememberMe: [true]
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.errorMsg.set(null);
    this.loading.set(true);

    const { userName, password, rememberMe } = this.form.getRawValue();
    this.auth.login(userName, password, rememberMe).subscribe({
      next: () => {
        this.loading.set(false);
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || this.auth.landingRoute();
        this.router.navigateByUrl(returnUrl);
      },
      error: err => {
        this.loading.set(false);
        this.errorMsg.set(err?.error?.Message || err?.message || 'Invalid credentials.');
      }
    });
  }
}
