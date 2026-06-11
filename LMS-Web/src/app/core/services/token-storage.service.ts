import { Injectable, PLATFORM_ID, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

const ACCESS_KEY = 'tsss.access';
const REFRESH_KEY = 'tsss.refresh';
const USER_KEY = 'tsss.user';

/**
 * SSR-safe persistence. All localStorage access is guarded with
 * isPlatformBrowser so the Express server render doesn't blow up.
 */
@Injectable({ providedIn: 'root' })
export class TokenStorageService {
  private platformId = inject(PLATFORM_ID);
  private get browser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  get accessToken(): string | null {
    return this.browser ? localStorage.getItem(ACCESS_KEY) : null;
  }

  get refreshToken(): string | null {
    return this.browser ? localStorage.getItem(REFRESH_KEY) : null;
  }

  getUser<T>(): T | null {
    if (!this.browser) return null;
    const raw = localStorage.getItem(USER_KEY);
    return raw ? (JSON.parse(raw) as T) : null;
  }

  setTokens(access: string, refresh: string): void {
    if (!this.browser) return;
    localStorage.setItem(ACCESS_KEY, access);
    localStorage.setItem(REFRESH_KEY, refresh);
  }

  setUser(user: unknown): void {
    if (!this.browser) return;
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  clear(): void {
    if (!this.browser) return;
    localStorage.removeItem(ACCESS_KEY);
    localStorage.removeItem(REFRESH_KEY);
    localStorage.removeItem(USER_KEY);
  }
}
