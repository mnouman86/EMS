import { Component, AfterViewInit } from '@angular/core';
import Swiper from 'swiper';

interface Testimonial {
  author: string;
  designation: string;
  content: string;
  rating: number;
}
@Component({
  selector: 'app-testimonials',
  standalone: false,
  templateUrl: './testimonials.html',
  styleUrl: './testimonials.scss'
})
export class Testimonials implements AfterViewInit{
 testimonials: Testimonial[] = [
    {
      author: 'David John',
      designation: 'Artist and Instructor',
      content: 'I would highly recommend Michael Richard to anyone interested the subject matter. It has provided me with invaluable knowledge & a newfound passion topic.',
      rating: 4
    },
    {
      author: 'Sarah Smith',
      designation: 'Parent',
      content: 'The STEM Sprout has been amazing for my child. The teachers are wonderful and the curriculum is engaging.',
      rating: 5
    },
    {
      author: 'Mike Johnson',
      designation: 'Engineer',
      content: 'As an engineer, I appreciate the hands-on approach to learning. My daughter loves going to school every day.',
      rating: 5
    }
  ];

  ngAfterViewInit() {
    this.initSwiper();
  }

  private initSwiper() {
    if (typeof window === 'undefined' || typeof document === 'undefined') {
      return;
    }

    const swiper = new Swiper('.mySwiper-testimonials', {
      spaceBetween: 30,
      slidesPerView: 1,
      loop: true,
      speed: 1500,
      navigation: {
        nextEl: '.swiper-button-next3',
        prevEl: '.swiper-button-prev3'
      },
      autoplay: {
        delay: 4000
      }
    });
  }
}
