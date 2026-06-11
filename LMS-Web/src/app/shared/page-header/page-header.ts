import { Component } from '@angular/core';

@Component({
  selector: 'app-page-header',
  standalone: false,
  templateUrl: './page-header.html',
  styleUrl: './page-header.scss'
})
export class PageHeader {
toggleMobileMenu() {
    // Add mobile menu toggle logic here
    const sideBar = document.getElementById('side-bar');
    if (sideBar) {
      sideBar.classList.toggle('open');
    }
  }
}
