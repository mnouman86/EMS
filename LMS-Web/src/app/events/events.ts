import { Component } from '@angular/core';

interface Event {
  day: string;
  month: string;
  title: string;
  time: string;
  location: string;
  link: string;
  color: string;
  bgClass: string;
}
@Component({
  selector: 'app-events',
  standalone: false,
  templateUrl: './events.html',
  styleUrl: './events.scss'
})
export class Events {
events: Event[] = [
    {
      day: '29',
      month: 'January',
      title: 'Annual Cultural Programme',
      time: '9:00 Am - 12:00 Pm',
      location: '55 Clark St, Brooklyn, NY 11201, USA',
      link: '/events/cultural-program',
      color: '',
      bgClass: 'bg-one'
    },
    {
      day: '05',
      month: 'March',
      title: 'A World of Stories Awaits',
      time: '9:00 Am - 12:00 Pm',
      location: '55 Clark St, Brooklyn, NY 11201, USA',
      link: '/events/stories',
      color: 'two',
      bgClass: 'bg-four'
    },
    {
      day: '11',
      month: 'March',
      title: 'World Drawing Day',
      time: '9:00 Am - 12:00 Pm',
      location: '55 Clark St, Brooklyn, NY 11201, USA',
      link: '/events/drawing-day',
      color: 'three',
      bgClass: 'bg-two'
    },
    {
      day: '19',
      month: 'April',
      title: 'World Kids Day',
      time: '9:00 Am - 12:00 Pm',
      location: '55 Clark St, Brooklyn, NY 11201, USA',
      link: '/events/kids-day',
      color: 'four',
      bgClass: 'bg-three'
    }
  ];
}
