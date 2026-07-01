import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnderConstruction } from './pages/under-construction/under-construction';

import { Home } from './home/home';
import { About } from './about/about';
import { Classes } from './classes/classes';
import { Blog } from './blog/blog';
import { Contact } from './contact/contact';
import { AboutPage } from './pages/about/about';
import { BlogPage } from './pages/blog/blog';
import { ContactPage } from './pages/contact/contact';
import { ClassesPage } from './pages/classes/classes';
import { ToddlerClass } from './pages/toddler-class/toddler-class';
import { PreschoolClass } from './pages/preschool-class/preschool-class';
import { PreKClass } from './pages/pre-k-class/pre-k-class';
import { KindergartenClass } from './pages/kindergarten-class/kindergarten-class';

import { PreNurseryDiscovery } from './pages/curriculum-basics/pre-nursery-discovery/pre-nursery-discovery';
import { NurseryExploration } from './pages/curriculum-basics/nursery-exploration/nursery-exploration';
import { KgFoundation } from './pages/curriculum-basics/kg-foundation/kg-foundation';

import { Grade1Growth } from './pages/curriculum-basics/grade-1-growth/grade-1-growth';
import { Grade2Expansion } from './pages/curriculum-basics/grade-2-expansion/grade-2-expansion';
import { Grade3CriticalThinking } from './pages/curriculum-basics/grade-3-critical-thinking/grade-3-critical-thinking';
import { Grade4Leadership } from './pages/curriculum-basics/grade-4-leadership/grade-4-leadership';
import { Grade5Synthesis } from './pages/curriculum-basics/grade-5-synthesis/grade-5-synthesis';
import { Grade6MasteryTransition } from './pages/curriculum-basics/grade-6-mastery-transition/grade-6-mastery-transition';
import { Grade7Advancement } from './pages/curriculum-basics/grade-7-advancement/grade-7-advancement';
import { Grade8Achievement } from './pages/curriculum-basics/grade-8-achievement/grade-8-achievement';
import { HomeLayout } from './layouts/home-layout/home-layout';
import { DefaultLayout } from './layouts/default-layout/default-layout';
import { CurriculumLayout } from './layouts/curriculum-layout/curriculum-layout';

const routes: Routes = [
  // Default startup route — the empty URL lands on the login screen on first
  // load. `pathMatch: 'full'` keeps this from swallowing the other top-level
  // `path: ''` parents (DefaultLayout / CurriculumLayout) that serve marketing
  // children like /about, /classes, /pre-nursery-discovery, etc.
  { path: '', pathMatch: 'full', redirectTo: 'login' },

  // Marketing home is still mounted at /home for anyone linking directly to it.
  {
    path: 'home',
    component: HomeLayout,
    children: [
      { path: '',
        component: Home,
        data: {
            title: 'STEM Sprout School | Joyful Learning in Islamabad',
            description: 'Pakistan’s first homework-free STEM-based school focused on creativity, confidence, and critical thinking.',
            keywords: 'STEM school Islamabad, preschool, kindergarten, joyful learning'
          }
       }
    ]
  },
  // { path: 'about', component: AboutPage },
  // { path: 'classes', component: ClassesPage },
  // { path: 'toddler', component: ToddlerClass },
  // { path: 'preschool', component: PreschoolClass },
  // { path: 'prek', component: PreKClass },
  // { path: 'kindergarten', component: KindergartenClass },
  // //{ path: 'classes/:type', component: ClassesPage },

  // // Curriculum Basics
  // { path: 'pre-nursery-discovery', component: PreNurseryDiscovery },
  // { path: 'nursery-exploration', component: NurseryExploration },
  // { path: 'kg-foundation', component: KgFoundation },

  // { path: 'grade-1-growth', component: Grade1Growth },
  // { path: 'grade-2-expansion', component: Grade2Expansion },
  // { path: 'grade-3-critical-thinking', component: Grade3CriticalThinking },
  // { path: 'grade-4-leadership', component: Grade4Leadership },
  // { path: 'grade-5-synthesis', component: Grade5Synthesis },
  // { path: 'grade-6-mastery-transition', component: Grade6MasteryTransition },
  // { path: 'grade-7-advancement', component: Grade7Advancement },
  // { path: 'grade-8-achievement', component: Grade8Achievement },


  // { path: 'blog', component: BlogPage },
  // { path: 'contact', component: ContactPage },
  {
    path: '',
    component: DefaultLayout,
    children: [
      { path: 'about', component: AboutPage,
        data: {
          title: 'About Us | STEM Sprout School',
          description: 'Learn about STEM Sprout School’s vision, philosophy, and revolutionary education system.',
          keywords: 'about STEM school, innovative education Pakistan'
        } },
      { path: 'classes', component: ClassesPage,
        data: {
          title: 'Classes | STEM Sprout School',
          description: 'Explore our curriculum from pre-nursery to grade 8 focused on growth and innovation.',
          keywords: 'school classes Pakistan, curriculum STEM'
        } },
      { path: 'blog', component: BlogPage },
      { path: 'contact', component: ContactPage },
      {
        path: 'apply',
        data: {
          public: true,
          title: 'Admission Application | STEM Sprout School',
          description: 'Apply for admission at STEM Sprout School — submit your child’s application online.'
        },
        loadComponent: () => import('./students/admission-form/admission-form').then(m => m.AdmissionForm)
      },
      {
        path: 'fee-status',
        data: {
          title: 'Fee Status | STEM Sprout School',
          description: 'Check your child’s current fee status online.'
        },
        loadComponent: () => import('./fees/parent-fee-status/parent-fee-status').then(m => m.ParentFeeStatus)
      },
      {
        path: 'result-status',
        data: {
          title: 'Result Status | STEM Sprout School',
          description: 'Check your child’s latest published result online.'
        },
        loadComponent: () => import('./results/parent-result/parent-result').then(m => m.ParentResult)
      }
    ]
  },
  {
  path: '',
  component: CurriculumLayout,
  children: [
    // curriculum basics
      { path: 'pre-nursery-discovery', component: PreNurseryDiscovery,
        data: {
  title: 'Pre-Nursery Discovery Program',
  description: 'Early childhood discovery-based learning program in Islamabad'
}
       },
      { path: 'nursery-exploration', component: NurseryExploration },
      { path: 'kg-foundation', component: KgFoundation },
      { path: 'grade-1-growth', component: Grade1Growth },
      { path: 'grade-2-expansion', component: Grade2Expansion },
      { path: 'grade-3-critical-thinking', component: Grade3CriticalThinking },
      { path: 'grade-4-leadership', component: Grade4Leadership },
      { path: 'grade-5-synthesis', component: Grade5Synthesis },
      { path: 'grade-6-mastery-transition', component: Grade6MasteryTransition },
      { path: 'grade-7-advancement', component: Grade7Advancement },
      { path: 'grade-8-achievement', component: Grade8Achievement }
  ]
},
  // Auth screens (standalone, lazy-loaded, no layout chrome)
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login').then(m => m.Login)
  },
  {
    path: 'forgot-password',
    loadComponent: () => import('./auth/forgot-password/forgot-password').then(m => m.ForgotPassword)
  },
  {
    path: 'reset-password',
    loadComponent: () => import('./auth/reset-password/reset-password').then(m => m.ResetPassword)
  },

  // Admin SPA subtree (standalone, lazy-loaded, auth-guarded)
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.routes').then(m => m.ADMIN_ROUTES)
  },
  // Parent portal subtree (standalone, lazy-loaded, auth-guarded)
  {
    path: 'parent',
    loadChildren: () => import('./parent/parent.routes').then(m => m.PARENT_ROUTES)
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
