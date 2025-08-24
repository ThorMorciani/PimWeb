import { Routes } from '@angular/router';
import { DashboardLayoutComponent } from './layouts/dashboard-layout/dashboard-layout.component';
import { HomeComponent } from './views/home/home.component';
import { UsuariosComponent } from './views/usuarios/usuarios.component';
import { TicketsComponent } from './views/tickets/tickets.component';
import { RelatoriosComponent } from './views/relatorios/relatorios.component';
import { AssuntosComponent } from './views/assuntos/assuntos.component';

export const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'tickets', component: TicketsComponent },
      { path: 'relatorios', component: RelatoriosComponent },
      { path: 'assuntos', component: AssuntosComponent },
    ],
  },
];
