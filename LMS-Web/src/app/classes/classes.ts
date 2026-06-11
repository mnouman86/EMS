import { Component } from '@angular/core';

interface ClassItem {
  title: string;
  period: string;
  description: string;
  icon: string;
  link: string;
  color: string;
}

@Component({
  selector: 'app-classes',
  standalone: false,
  templateUrl: './classes.html',
  styleUrl: './classes.scss'
})
export class Classes {
  classes: ClassItem[] = [
    {
      title: 'Pre-Nursery (Discovery)',
      period: '(3 - 4 years)',
      description: 'Tiny steps, big discoveries every day.',
      icon: 'assets/images/icon/03.svg',
      link: '/pre-nursery-discovery',
      color: ''
    },
    {
      title: 'Nursery (Exploration)',
      period: '(4 - 5 years)',
      description: 'Where curiosity blooms into early learning.',
      icon: 'assets/images/icon/04.svg',
      link: '/nursery-exploration',
      color: 'two'
    },
    {
      title: 'Kindergarten (Foundation)',
      period: '(5 - 6 years)',
      description: 'Building bright minds with playful foundations.',
      icon: 'assets/images/icon/05.svg',
      link: '/kg-foundation',
      color: 'three'
    },
    {
      title: 'Grade 1 (Growth)',
      period: '(6 - 7 years)',
      description: 'Preparing young explorers for the journey ahead.',
      icon: 'assets/images/icon/06.svg',
      link: '/grade-1-growth',
      color: 'four'
    }
  ];
}