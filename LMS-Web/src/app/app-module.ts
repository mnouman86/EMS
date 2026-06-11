import { NgModule, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { definePreset } from '@primeng/themes';
import { MessageService } from 'primeng/api';

/** TSSS brand theme — teal primary to match the school identity. */
const TsssPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50: '#f0fdfa', 100: '#ccfbf1', 200: '#99f6e4', 300: '#5eead4', 400: '#2dd4bf',
      500: '#14b8a6', 600: '#0d9488', 700: '#0f766e', 800: '#115e59', 900: '#134e4a', 950: '#042f2e'
    }
  }
});

import { authInterceptor } from './core/interceptors/auth.interceptor';
import { errorInterceptor } from './core/interceptors/error.interceptor';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Header } from './header/header';
import { Banner } from './banner/banner';
import { About } from './about/about';
import { Classes } from './classes/classes';
import { Courses } from './courses/courses';
import { Testimonials } from './testimonials/testimonials';
import { Blog } from './blog/blog';
import { Footer } from './footer/footer';
import { Home } from './home/home';
import { Contact } from './contact/contact';
import { Category } from './category/category';
import { Events } from './events/events';
import { AboutPage } from './pages/about/about';
import { PageHeader } from './shared/page-header/page-header';
import { PageFooter } from './shared/page-footer/page-footer';
import { Breadcrumb } from './shared/breadcrumb/breadcrumb';
import { ContactPage } from './pages/contact/contact';
import { BlogPage } from './pages/blog/blog';
import { ToddlerClass } from './pages/toddler-class/toddler-class';
import { PreschoolClass } from './pages/preschool-class/preschool-class';
import { KindergartenClass } from './pages/kindergarten-class/kindergarten-class';
import { PreKClass } from './pages/pre-k-class/pre-k-class';
import { UnderConstruction } from './pages/under-construction/under-construction';
import { PreNurseryDiscovery } from './pages/curriculum-basics/pre-nursery-discovery/pre-nursery-discovery';
import { NurseryExploration } from './pages/curriculum-basics/nursery-exploration/nursery-exploration';
import { KgFoundation } from './pages/curriculum-basics/kg-foundation/kg-foundation';
import { Grade2Expansion } from './pages/curriculum-basics/grade-2-expansion/grade-2-expansion';
import { Grade3CriticalThinking } from './pages/curriculum-basics/grade-3-critical-thinking/grade-3-critical-thinking';
import { Grade4Leadership } from './pages/curriculum-basics/grade-4-leadership/grade-4-leadership';
import { Grade5Synthesis } from './pages/curriculum-basics/grade-5-synthesis/grade-5-synthesis';
import { Grade6MasteryTransition } from './pages/curriculum-basics/grade-6-mastery-transition/grade-6-mastery-transition';
import { Grade7Advancement } from './pages/curriculum-basics/grade-7-advancement/grade-7-advancement';
import { Grade8Achievement } from './pages/curriculum-basics/grade-8-achievement/grade-8-achievement';
import { Grade1Growth } from './pages/curriculum-basics/grade-1-growth/grade-1-growth';
import { HomeLayout } from './layouts/home-layout/home-layout';
import { DefaultLayout } from './layouts/default-layout/default-layout';
import { CurriculumLayout } from './layouts/curriculum-layout/curriculum-layout';

@NgModule({
  declarations: [
    App,
    Header,
    Banner,
    About,
    Classes,
    Courses,
    Testimonials,
    Blog,
    Footer,
    Home,
    Contact,
    Category,
    Events,
    AboutPage,
    ContactPage,
    BlogPage,
    PageHeader,
    PageFooter,
    Breadcrumb,
    ToddlerClass,
    PreschoolClass,
    KindergartenClass,
    PreKClass,
    UnderConstruction,
    PreNurseryDiscovery,
    NurseryExploration,
    KgFoundation,
    Grade2Expansion,
    Grade3CriticalThinking,
    Grade4Leadership,
    Grade5Synthesis,
    Grade6MasteryTransition,
    Grade7Advancement,
    Grade8Achievement,
    Grade1Growth,
    HomeLayout,
    DefaultLayout,
    CurriculumLayout 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch(), withInterceptors([authInterceptor, errorInterceptor])),
    provideAnimationsAsync(),
    providePrimeNG({ theme: { preset: TsssPreset, options: { darkModeSelector: '.app-dark' } } }),
    MessageService
  ],
  bootstrap: [App]
})
export class AppModule { }
