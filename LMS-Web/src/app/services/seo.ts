import { Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class Seo {
  constructor(private title: Title, private meta: Meta) {}

  updateSeo(data: {
    title?: string;
    description?: string;
    keywords?: string;
  }) {
    if (data.title) {
      this.title.setTitle(data.title);
    }

    if (data.description) {
      this.meta.updateTag({
        name: 'description',
        content: data.description
      });
    }

    if (data.keywords) {
      this.meta.updateTag({
        name: 'keywords',
        content: data.keywords
      });
    }
  }
}
