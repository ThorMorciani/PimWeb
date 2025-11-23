import { Routes } from '@angular/router';
import { DashboardLayoutComponent } from './layouts/dashboard-layout/dashboard-layout.component';
import { HomeComponent } from './views/home/home.component';
import { UsuariosComponent } from './views/usuarios/usuarios.component';
import { TicketsComponent } from './views/tickets/tickets-list/tickets.component';
import { NovoTicketComponent } from './views/tickets/novo-ticket/novo-ticket.component';
import { VisualizarTicketComponent } from './views/tickets/view-ticket/visualizar-ticket.component';
import { RelatoriosComponent } from './views/relatorios/relatorios.component';
import { LoginComponent } from './views/login/login.component';
import { RootCausesComponent } from './views/root-causes/root-causes.component';
import { PerfisComponent } from './views/perfis/perfis.component';

import { AuthGuard } from '../core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'auth',
    children: [
      { path: 'login', component: LoginComponent }
    ],
  },
  {
    path: '',
    component: DashboardLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'tickets', component: TicketsComponent },
      { path: 'tickets/novo', component: NovoTicketComponent },
      { path: 'tickets/:ticketNumber', component: VisualizarTicketComponent },
      { path: 'assuntos', component: RootCausesComponent },
      { path: 'relatorios', component: RelatoriosComponent },
      { path: 'perfis', component: PerfisComponent },
    ],
  },
];
