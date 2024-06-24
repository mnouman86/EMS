import { Component } from '@angular/core';
import { FileUploadService } from '../file-upload.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.css'
})
export class FileUploadComponent {
  selectedFile: File | null = null;
  message: string = '';

  constructor(private fileUploadService: FileUploadService) { }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  onUpload(): void {
    if (this.selectedFile) {
      this.fileUploadService.uploadFile(this.selectedFile).subscribe(
        (event) => {
          if (event.status === 'progress') {
            this.message = event.message;
          } else {
            this.message = `File uploaded successfully. Path: ${event.dbPath}`;
          }
        },
        (error) => {
          this.message = error.message;
        }
      );
    } else {
      this.message = 'Please select a file first.';
    }
  }
}
