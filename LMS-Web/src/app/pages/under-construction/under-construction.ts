import { Component } from '@angular/core';

@Component({
  selector: 'app-under-construction',
  standalone: false,
  templateUrl: './under-construction.html',
  styleUrl: './under-construction.scss'
})
export class UnderConstruction {
onSubmit(event: Event) {
    event.preventDefault();
    const emailInput = document.getElementById('email') as HTMLInputElement;
    const email = emailInput.value;
    
    if (email && email !== 'Your Email and Get Notified...') {
      // Here you can add your email submission logic
      console.log('Email submitted:', email);
      alert('Thank you! We will notify you when we launch.');
      emailInput.value = 'Your Email and Get Notified...';
    } else {
      alert('Please enter a valid email address.');
    }
  }

  onInputFocus(event: FocusEvent) {
    const input = event.target as HTMLInputElement;
    if (input.value === 'Your Email and Get Notified...') {
      input.value = '';
    }
  }

  onInputBlur(event: FocusEvent) {
    const input = event.target as HTMLInputElement;
    if (input.value === '') {
      input.value = 'Your Email and Get Notified...';
    }
  }
}
