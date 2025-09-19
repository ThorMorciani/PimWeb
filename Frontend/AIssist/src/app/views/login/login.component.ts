import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [CommonModule, ReactiveFormsModule] // 👈 precisa disso
})
export class LoginComponent {
  form: FormGroup;
  loading = false;
  errorMessage = '';

  constructor(private fb: FormBuilder, private router: Router) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
  return this.form.controls;
}

  login() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    // --- simulação de login fake ---
    setTimeout(() => {
      const { email, password } = this.form.value;

      if (email === 'admin@teste.com' && password === '123456') {
        // salva usuário fake no localStorage
        localStorage.setItem('user', JSON.stringify({ email, token: 'fake-jwt-token' }));
        this.router.navigate(['/']); // redireciona pro dashboard
      } else {
        this.errorMessage = 'Credenciais inválidas';
      }

      this.loading = false;
    }, 1000);
  } 
}
