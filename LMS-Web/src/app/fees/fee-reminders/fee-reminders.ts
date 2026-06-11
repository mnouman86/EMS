import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';
import { MultiSelectModule } from 'primeng/multiselect';
import { TextareaModule } from 'primeng/textarea';
import { MessageModule } from 'primeng/message';
import { FeeService } from '../fee.service';
import { SendRemindersResult } from '../fee.models';
import { StudentService } from '../../students/student.service';
import { defaultSearch } from '../../core/models/search-request';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-fee-reminders',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, ButtonModule, SelectModule, MultiSelectModule, TextareaModule, MessageModule],
  templateUrl: './fee-reminders.html'
})
export class FeeReminders implements OnInit {
  private svc = inject(FeeService);
  private studentSvc = inject(StudentService);
  private toast = inject(ToastService);

  channelOptions = [
    { label: 'WhatsApp', value: 'Whatsapp' },
    { label: 'SMS', value: 'Sms' },
    { label: 'Both', value: 'Both' }
  ];
  studentOptions = signal<{ label: string; value: number }[]>([]);

  channel = 'Whatsapp';
  studentIds: number[] = [];
  customBody = '';

  sending = signal(false);
  result = signal<SendRemindersResult | null>(null);

  ngOnInit(): void {
    this.studentSvc.getAll(defaultSearch()).subscribe(res => {
      this.studentOptions.set((res.data ?? []).map(s => ({ label: `${s.fullName} (${s.studentCode || s.formNo || '—'})`, value: s.id })));
    });
  }

  send(): void {
    this.sending.set(true);
    this.result.set(null);
    this.svc.sendReminders({
      studentIds: this.studentIds.length ? this.studentIds : undefined,
      channel: this.channel,
      customBody: this.customBody || null
    }).subscribe({
      next: res => { this.result.set(res.data ?? null); this.sending.set(false); this.toast.success('Reminders dispatched'); },
      error: () => this.sending.set(false)
    });
  }
}
