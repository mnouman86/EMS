import { Component } from '@angular/core';
interface RecentPost {
  title: string;
  image: string;
  date: string;
  link: string;
}

@Component({
  selector: 'app-page-footer',
  standalone: false,
  templateUrl: './page-footer.html',
  styleUrl: './page-footer.scss'
})
export class PageFooter {
recentPosts: RecentPost[] = [
    {
      title: 'Avoid These 4 Common When Managing Remote Teams',
      image: 'assets/images/blog/blog-07.jpg',
      date: 'October 29, 2023',
      link: '/blog/remote-teams'
    },
    {
      title: 'How To Draw Realistic Lips In 7 Simple Steps',
      image: 'assets/images/blog/blog-08.jpg',
      date: 'October 29, 2023',
      link: '/blog/drawing-lips'
    }
  ];
}
