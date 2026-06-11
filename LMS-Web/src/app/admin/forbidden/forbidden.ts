import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-forbidden',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div style="text-align:center;padding:60px 20px;">
      <i class="pi pi-lock" style="font-size:3rem;color:#ef4444;"></i>
      <h2 style="margin:16px 0 8px;">403 — Access denied</h2>
      <p style="color:#64748b;">Your role does not have permission to view this page.</p>
      <a routerLink="/admin/dashboard" class="p-button p-component" style="margin-top:16px;display:inline-block;">
        Back to dashboard
      </a>
    </div>
  `
})
export class Forbidden {}
