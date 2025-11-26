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
import { RoleGuard } from '../core/guards/role.guard';
import { LogsComponent } from './views/logs/logs.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  
  {
    path: 'not-authorized',
    loadComponent: () =>
      import('./views/not-authorized/not-authorized.component').then(
        (m) => m.NotAuthorizedComponent
      ),
  },

  {
    path: '',
    component: DashboardLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { 
        path: '', 
        component: HomeComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente', 'Tecnico', 'Usuario'] }
      },

      {
        path: 'usuarios',
        component: UsuariosComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador'] }
      },

      {
        path: 'tickets',
        component: TicketsComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente', 'Tecnico', 'Usuario'] }
      },

      {
        path: 'tickets/novo',
        component: NovoTicketComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente', 'Tecnico', 'Usuario'] }
      },

      {
        path: 'tickets/:ticketNumber',
        component: VisualizarTicketComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente', 'Tecnico', 'Usuario'] }
      },

      {
        path: 'assuntos',
        component: RootCausesComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador'] }
      },

      {
        path: 'relatorios',
        component: RelatoriosComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente'] }
      },

      {
        path: 'perfis',
        component: PerfisComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador', 'Gerente' ] }
      },
      {
        path: 'logs',
        component: LogsComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Administrador'] }
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
