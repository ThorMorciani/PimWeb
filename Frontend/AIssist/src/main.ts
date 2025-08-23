import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { importProvidersFrom } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';

import { HomeComponent } from './app/home/home.component';

bootstrapApplication(HomeComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
  ]
}).catch(err => console.error(err));
