import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private http: HttpClient) {}

  private apiUrl = environment.baseUrl;

  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Login`, data);
  }

  isLogged(): boolean {
    if (typeof window === 'undefined') return false; // verifica se está no navegador
    return !!localStorage.getItem('token');
  }

  logout() {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    }
  }
}
