import { Component, AfterViewInit } from '@angular/core';
import Swiper from 'swiper';

interface Course {
  title: string;
  description: string;
  image: string;
  link: string;
  features: string[];
  color: string;
}

@Component({
  selector: 'app-courses',
  standalone: false,
  templateUrl: './courses.html',
  styleUrl: './courses.scss'
})
export class Courses implements AfterViewInit {
  courses: Course[] = [
    {
      title: 'Early Explorers (Ages 3-5)',
      description: 'A joyful foundation in English, Math, Science, Technology — paired with Urdu, Islamic values, and general knowledge adventures.',
      image: 'assets/images/course/01.jpg',
      link: '/early-explorers',
      features: ['Learning Adventure', 'Little Scientists'],
      color: ''
    },
    {
      title: 'Self Learners (Ages 6-11)',
      description: 'Exploring English, Math, Science, Urdu, Islam, Coding, and more — sparking curiosity, creativity, and confidence.',
      image: 'assets/images/course/02.jpg',
      link: '/self-learners',
      features: ['Math Mania', 'Science Safari'],
      color: 'two'
    },
    {
      title: 'Primary School (Ages 12-14)',
      description: 'Empowering young minds with advanced learning, creative projects, and values that shape independent thinkers',
      image: 'assets/images/course/03.jpg',
      link: '/primary-school',
      features: ['Coding', 'Robotics'],
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

    const swiper = new Swiper('.mySwiper-category-1', {
      spaceBetween: 30,
      slidesPerView: 3,
      loop: true,
      speed: 1500,
      pagination: {
        el: '.swiper-paginations',
        clickable: true
      },
      autoplay: {
        delay: 4000
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