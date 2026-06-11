import { Component } from '@angular/core';

interface ContactFormData {
  name: string;
  email: string;
  phone: string;
  subject: string;
  message: string;
}

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.html',
  styleUrl: './contact.scss'
})
export class ContactPage {
  formData: ContactFormData = {
    name: '',
    email: '',
    phone: '',
    subject: '',
    message: ''
  };

  isSubmitting = false;
  submitMessage = '';
  submitSuccess = false;

  onSubmit() {
    if (this.isSubmitting) {
      return;
    }

    this.isSubmitting = true;
    this.submitMessage = '';

    // Simulate form submission
    // Replace this with actual API call to your backend
    console.log('Form submitted:', this.formData);

    // Simulate API delay
    setTimeout(() => {
      // Success case
      this.submitSuccess = true;
      this.submitMessage = 'Thank you for contacting us! We will get back to you soon.';
      this.isSubmitting = false;

      // Reset form after successful submission
      this.resetForm();

      // Clear success message after 5 seconds
      setTimeout(() => {
        this.submitMessage = '';
      }, 5000);

      // Error case example (uncomment to test)
      // this.submitSuccess = false;
      // this.submitMessage = 'Sorry, there was an error sending your message. Please try again.';
      // this.isSubmitting = false;
    }, 1500);
  }

  private resetForm() {
    this.formData = {
      name: '',
      email: '',
      phone: '',
      subject: '',
      message: ''
    };
  }
}