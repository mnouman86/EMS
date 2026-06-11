import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
toggleMobileMenu() {
    // Add your mobile menu toggle logic here
    const sideBar = document.getElementById('side-bar');
    if (sideBar) {
      sideBar.classList.toggle('open');
    }
  }
}
