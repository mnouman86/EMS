import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  // Admin SPA + auth screens are client-rendered (auth-gated, parameterised, no SEO value).
  { path: 'admin', renderMode: RenderMode.Client },
  { path: 'admin/**', renderMode: RenderMode.Client },
  { path: 'parent', renderMode: RenderMode.Client },
  { path: 'parent/**', renderMode: RenderMode.Client },
  { path: 'login', renderMode: RenderMode.Client },
  { path: 'forgot-password', renderMode: RenderMode.Client },
  { path: 'reset-password', renderMode: RenderMode.Client },

  // Admission form is interactive (loads live classes, posts to API) — client-render.
  { path: 'apply', renderMode: RenderMode.Client },

  // Parent fee status is interactive (posts to API) — client-render.
  { path: 'fee-status', renderMode: RenderMode.Client },

  // Parent result status is interactive (posts to API) — client-render.
  { path: 'result-status', renderMode: RenderMode.Client },

  // Public marketing pages keep SSR prerendering for SEO.
  { path: '**', renderMode: RenderMode.Prerender }
];
