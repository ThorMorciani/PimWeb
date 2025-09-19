import { Routes } from '@angular/router';

// Layouts
import { DashboardLayoutComponent } from './layouts/dashboard-layout/dashboard-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

// Components
import { HomeComponent } from './views/home/home.component';
import { UsuariosComponent } from './views/usuarios/usuarios.component';
import { TicketsComponent } from './views/tickets/tickets.component';
import { RelatoriosComponent } from './views/relatorios/relatorios.component';
import { AssuntosComponent } from './views/assuntos/assuntos.component';
import { LoginComponent } from './views/login/login.component';
import { EmailRecComponent } from './views/email.rec/email.rec.component';
import { CodRecuperacaoComponent } from './views/cod-recuperacao/cod-recuperacao.component';

export const routes: Routes = [
  // Rotas do dashboard (usuário logado)
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

  // Rotas de autenticação
  {
    path: 'teste',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'email-rec', component: EmailRecComponent },          // renomeado para não ter ponto
      { path: 'cod-recuperacao', component: CodRecuperacaoComponent } // nome único
    ],
  },

  // fallback → se não achar rota, vai pro login
  { path: '**', redirectTo: 'login' }
];
