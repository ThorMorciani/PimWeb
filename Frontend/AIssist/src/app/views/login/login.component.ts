// src/app/views/login/login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import type { User } from '../../types/User';
import type { AuthResponse } from '../../types/Auth';
import { ConfirmModalComponent } from '../../shared/components/confirm-modal/confirm-modal.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    RouterModule,
    ConfirmModalComponent
  ],
})
export class LoginComponent {
  loginData = { username: '', password: '' };
  showModal = false;
  showOkButton = false;
  modalMessage = '';
  modalTitleMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (res: AuthResponse) => {
        const user: User = {
          id: res.id,
          name: res.name,
          username: res.username,
          email: res.email,
          profile: res.profile
        };

        localStorage.setItem('token', res.accessToken);
        localStorage.setItem('user', JSON.stringify(user));
        this.router.navigate(['/']);
      },
      error: () => {
        this.modalMessage = 'Falha ao criar usuário.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      },
    });
  }

  onCancelAction() {
    this.showModal = false;
    this.showOkButton = false;
  }
}
