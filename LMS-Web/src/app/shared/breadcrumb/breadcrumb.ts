import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-breadcrumb',
  standalone: false,
  templateUrl: './breadcrumb.html',
  styleUrl: './breadcrumb.scss'
})
export class Breadcrumb {
 @Input() pageTitle: string = '';
  @Input() currentRoute: string = '';
}
