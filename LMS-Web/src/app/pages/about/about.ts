import { Component, AfterViewInit } from '@angular/core';

interface Counter {
  value: number;
  suffix: string;
  label: string;
}

@Component({
  selector: 'app-aboutpage',
  standalone: false,
  templateUrl: './about.html',
  styleUrl: './about.scss'
})
export class AboutPage implements AfterViewInit {
  isVideoPlaying = false;
  
  counters: Counter[] = [
    { value: 0, suffix: '', label: 'Admission Fee' },
    { value: 100, suffix: '%', label: 'Bag-Free Learning' },
    { value: 0, suffix: '', label: 'Homework, Ever' },
    { value: 1, suffix: '+', label: 'Year(s) Ahead' }
  ];

  playVideo() {
    this.isVideoPlaying = true;
  }

  closeVideo() {
    this.isVideoPlaying = false;
  }

  ngAfterViewInit() {
    // Initialize counter animation if needed
    this.initializeCounters();
  }

  private initializeCounters() {
    // Add counter animation logic here if using a library
    // For now, we'll just display the static numbers
  }
}