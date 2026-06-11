import { Component, AfterViewInit } from '@angular/core';
import Swiper from 'swiper';

interface ICategory {
  title: string;
  icon: string;
  link: string;
  color: string;
}

@Component({
  selector: 'app-category',
  standalone: false,
  templateUrl: './category.html',
  styleUrl: './category.scss'
})
export class Category implements AfterViewInit {
  categories: ICategory[] = [
    {
      title: 'Languages',
      icon: 'assets/images/icon/07.svg',
      link: '/languages',
      color: ''
    },
    {
      title: 'Coding',
      icon: 'assets/images/icon/08.svg',
      link: '/coding',
      color: 'two'
    },
    {
      title: 'Math',
      icon: 'assets/images/icon/09.svg',
      link: '/math',
      color: 'three'
    },
    {
      title: 'Music',
      icon: 'assets/images/icon/10.svg',
      link: '/music',
      color: 'four'
    },
    {
      title: 'Writing',
      icon: 'assets/images/icon/11.svg',
      link: '/writing',
      color: 'five'
    }
  ];

  ngAfterViewInit() {
    this.initSwiper();
  }

  private initSwiper() {
    if (typeof window === 'undefined' || typeof document === 'undefined') {
      return;
    }

    const swiper = new Swiper('.mySwiper-category-2', {
      spaceBetween: 80,
      slidesPerView: 5,
      loop: true,
      speed: 1500,
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
      },
      pagination: {
        el: '.swiper-pagination2',
        clickable: true
      },
      autoplay: {
        delay: 4000
      },
      breakpoints: {
        0: { slidesPerView: 1, spaceBetween: 30 },
        320: { slidesPerView: 1, spaceBetween: 80 },
        500: { slidesPerView: 2, spaceBetween: 80 },
        640: { slidesPerView: 2, spaceBetween: 80 },
        840: { slidesPerView: 3, spaceBetween: 80 },
        1140: { slidesPerView: 5, spaceBetween: 80 }
      }
    });
  }
}