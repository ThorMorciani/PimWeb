import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../../core/services/auth/auth.service';
import type { User } from '../../types/User';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterModule,
    MatIconModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  user: User | null = null;
  
  constructor(private auth: AuthService) {
    this.loadUserFromStorage();
  }

  get logged() {
    return this.auth.isLogged();
  }

  logout() {
    this.auth.logout();
    window.location.href = '/login';
  }

  getUserInitials(): string {
    if (!this.user || !this.user.name) return '';
    const names = this.user.name.trim().split(' ');
    const firstInitial = names[0].charAt(0);
    const lastInitial = names.length > 1 ? names[names.length - 1].charAt(0) : '';
    return (firstInitial + lastInitial).toUpperCase();
  }

  private loadUserFromStorage() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      this.user = JSON.parse(userJson) as User;
    }
  }
}
