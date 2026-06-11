import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ScriptLoader {
  loadScripts() {
    if (typeof window === 'undefined' || typeof document === 'undefined') {
      return;
    }

    setTimeout(() => {
      // use window to be explicit
      const win = window as any;
      if (typeof win.$ !== 'undefined' || typeof win.jQuery !== 'undefined') {
        this.initializePlugins();
      } else {
        console.warn('jQuery not available');
      }
    }, 1000);
  }

  private initializePlugins() {
    // Initialize Swiper (global)
    if (typeof (window as any).Swiper !== 'undefined') {
      const swipers = document.querySelectorAll('.swiper-data');
      swipers.forEach((swiperEl: any) => {
        const raw = swiperEl.getAttribute('data-swiper') || '{}';
        let config = {};
        try { config = JSON.parse(raw); } catch (e) { console.warn('Invalid data-swiper JSON', e); }
        new (window as any).Swiper(swiperEl, config);
      });
    } else {
      console.warn('Swiper not found');
    }

    // other plugin initialization (using global $)
    if (typeof (window as any).$ !== 'undefined') {
      const $ = (window as any).$;
      // e.g., $('.your-class').plugin();
    }
  }
}
