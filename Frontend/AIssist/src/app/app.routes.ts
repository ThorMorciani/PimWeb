import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./home/home.component').then(mod => mod.HomeComponent)
    },
    {
        path: '**',
        loadComponent: () => import('./not-found/not-found.component').then(mod => mod.NotFoundComponent)
    }
];
