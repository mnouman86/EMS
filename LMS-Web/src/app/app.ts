import { Component, OnInit, signal } from '@angular/core';
import { ScriptLoader } from './services/script-loader';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { Seo } from './services/seo';


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss'
})
export class App implements OnInit {
  protected readonly title = signal('LMS-Web');
  constructor(
    private scriptLoader: ScriptLoader,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private seo: Seo
  ) {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd)
      )
      .subscribe(() => {
        let route = this.activatedRoute;

        while (route.firstChild) {
          route = route.firstChild;
        }

        const data = route.snapshot.data;
        if (data) {
          this.seo.updateSeo(data);
        }
      });
  }
  
  ngOnInit() {
    this.scriptLoader.loadScripts();
  }
}
