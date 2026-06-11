import { Component } from '@angular/core';

@Component({
  selector: 'app-banner',
  standalone: false,
  templateUrl: './banner.html',
  styleUrl: './banner.scss'
})
export class Banner {
isVideoPlaying = false;

  playVideo() {
    this.isVideoPlaying = true;
  }

  closeVideo() {
    this.isVideoPlaying = false;
  }
}
