import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../../core/services/auth/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  user = this.auth.getUsuarioAtual();

  constructor(private auth: AuthService) {}

  logout() {
    this.auth.logout();
    window.location.href = '/login';
  }

  // Permissões
  isAdmin() { return this.auth.isAdmin(); }
  isGerente() { return this.auth.isGerente(); }
  isTecnico() { return this.auth.isTecnico(); }
  isUsuario() { return this.auth.isUsuario(); }

  getUserInitials(): string {
    if (!this.user) return '';
    const parts = this.user.name.split(' ');
    return (parts[0][0] + (parts[1]?.[0] ?? '')).toUpperCase();
  }
}
