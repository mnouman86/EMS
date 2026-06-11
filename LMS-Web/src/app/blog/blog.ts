import { Component, AfterViewInit } from '@angular/core';
import Swiper from 'swiper';

interface BlogPost {
  title: string;
  image: string;
  date: string;
  views: string;
  link: string;
  color: string;
}

@Component({
  selector: 'app-blog',
  standalone: false,
  templateUrl: './blog.html',
  styleUrl: './blog.scss'
})
export class Blog  implements AfterViewInit {
 blogPosts: BlogPost[] = [
    {
      title: 'How to spark passion-led, interest-based creativity.',
      image: 'assets/images/blog/blog-01.jpg',
      date: '28 December',
      views: '24k',
      link: '/blog/passion-creativity',
      color: ''
    },
    {
      title: 'Ways to Create A Montessori Home Environment',
      image: 'assets/images/blog/blog-02.jpg',
      date: '28 December',
      views: '24k',
      link: '/blog/montessori-home',
      color: 'two'
    },
    {
      title: 'Outschool mom helped her son discover his superpower.',
      image: 'assets/images/blog/blog-03.jpg',
      date: '28 December',
      views: '24k',
      link: '/blog/superpower-discovery',
      color: 'three'
    }
  ];

  ngAfterViewInit() {
    this.initSwiper();
  }

  private initSwiper() {
    if (typeof window === 'undefined' || typeof document === 'undefined') {
      return;
    }

    const swiper = new Swiper('.mySwiper-blog', {
      spaceBetween: 30,
      slidesPerView: 3,
      loop: true,
      speed: 1500,
      pagination: {
        el: '.swiper-pagination4',
        clickable: true
      },
      autoplay: {
        delay: 4500
      },
      breakpoints: {
        0: { slidesPerView: 1, spaceBetween: 30 },
        320: { slidesPerView: 1, spaceBetween: 30 },
        480: { slidesPerView: 1, spaceBetween: 30 },
        768: { slidesPerView: 2, spaceBetween: 30 },
        840: { slidesPerView: 2, spaceBetween: 30 },
        1140: { slidesPerView: 3, spaceBetween: 30 }
      }
    });
  }
}
