import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { MessageModule } from 'primeng/message';
import { StudentService } from './student.service';
import { StudentImportRow } from './student.models';
import { ToastService } from '../core/services/toast.service';

@Component({
  selector: 'app-student-import',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TableModule, ButtonModule, ToggleSwitchModule, MessageModule],
  templateUrl: './student-import.html'
})
export class StudentImport {
  private svc = inject(StudentService);
  private router = inject(Router);
  private toast = inject(ToastService);

  rows = signal<StudentImportRow[]>([]);
  fileName = signal<string | null>(null);
  parseError = signal<string | null>(null);
  updateExisting = false;
  saving = signal(false);

  /** Maps a tolerant header key to the StudentImportRow field. */
  private readonly headerMap: Record<string, keyof StudentImportRow> = {
    srno: 'srNo', sr: 'srNo', no: 'srNo',
    studentid: 'studentId', code: 'studentId', studentcode: 'studentId',
    formno: 'formNo', form: 'formNo',
    studentname: 'studentName', name: 'studentName', fullname: 'studentName',
    fathername: 'fatherName', father: 'fatherName', parentname: 'fatherName', parent: 'fatherName',
    emergencycontact: 'emergencyContact', emergency: 'emergencyContact', contact: 'emergencyContact', phone: 'emergencyContact',
    classname: 'className', class: 'className', grade: 'className'
  };

  onFileSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.fileName.set(file.name);
    this.parseError.set(null);
    const reader = new FileReader();
    reader.onload = () => {
      try { this.parseCsv(String(reader.result ?? '')); }
      catch (e) { this.parseError.set('Could not parse the file. Make sure it is a valid CSV.'); this.rows.set([]); }
    };
    reader.readAsText(file);
  }

  private splitCsvLine(line: string): string[] {
    const out: string[] = [];
    let cur = '';
    let inQuotes = false;
    for (let i = 0; i < line.length; i++) {
      const ch = line[i];
      if (inQuotes) {
        if (ch === '"' && line[i + 1] === '"') { cur += '"'; i++; }
        else if (ch === '"') inQuotes = false;
        else cur += ch;
      } else if (ch === '"') inQuotes = true;
      else if (ch === ',') { out.push(cur); cur = ''; }
      else cur += ch;
    }
    out.push(cur);
    return out.map(s => s.trim());
  }

  private parseCsv(text: string): void {
    const lines = text.split(/\r?\n/).filter(l => l.trim().length > 0);
    if (lines.length < 2) { this.parseError.set('File has no data rows.'); this.rows.set([]); return; }

    const rawHeaders = this.splitCsvLine(lines[0]);
    const fields = rawHeaders.map(h => this.headerMap[h.toLowerCase().replace(/[^a-z0-9]/g, '')] ?? null);

    const parsed: StudentImportRow[] = [];
    for (let i = 1; i < lines.length; i++) {
      const cells = this.splitCsvLine(lines[i]);
      const row: StudentImportRow = {};
      fields.forEach((f, idx) => {
        if (!f) return;
        const val = cells[idx] ?? '';
        if (f === 'srNo') row.srNo = val ? Number(val) : null;
        else (row as any)[f] = val || null;
      });
      if (row.studentName || row.studentId || row.formNo) parsed.push(row);
    }
    this.rows.set(parsed);
    if (parsed.length === 0) this.parseError.set('No usable rows found. Check that a "StudentName" column exists.');
  }

  clear(): void {
    this.rows.set([]);
    this.fileName.set(null);
    this.parseError.set(null);
  }

  submit(): void {
    const data = this.rows();
    if (data.length === 0) { this.toast.warn('Nothing to import.'); return; }
    this.saving.set(true);
    this.svc.bulkImport(data, this.updateExisting).subscribe({
      next: () => { this.toast.success(`Imported ${data.length} student(s)`); this.saving.set(false); this.router.navigate(['/admin/students']); },
      error: () => this.saving.set(false)
    });
  }
}
