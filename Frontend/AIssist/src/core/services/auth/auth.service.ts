import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import type { User } from '../../../app/types/User';

@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private http: HttpClient) {}

  private apiUrl = environment.baseUrl;

  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Login`, data);
  }

  isLogged(): boolean {
    if (typeof window === 'undefined') return false;
    return !!localStorage.getItem('token');
  }

  logout() {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    }
  }

  getUsuarioAtual(): User | null {
    const userJson = localStorage.getItem('user');
    if (!userJson) return null;

    try {
      const user = JSON.parse(userJson) as User;

      user.profile = this.normalizeRole(user.profile);

      return user;
    } catch (error) {
      console.error('Erro ao ler usuario logado do localStorage:', error);
      return null;
    }
  }

  // Remove acentos dos cargos e joga para minusculo
  normalizeRole(role: string): string {
    return role
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toLowerCase();
  }

  getRole(): string {
    return this.getUsuarioAtual()?.profile || '';
  }

  isAdmin(): boolean {
    return this.getRole() === 'administrador';
  }

  isGerente(): boolean {
    return this.getRole() === 'gerente';
  }

  isTecnico(): boolean {
    return this.getRole() === 'tecnico';
  }

  isUsuario(): boolean {
    return this.getRole() === 'usuario';
  }
}
