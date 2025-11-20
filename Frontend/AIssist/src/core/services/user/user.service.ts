import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface UserResponse {
  id: number;
  name: string;
  username: string;
  email: string;
  active: number;
  profile: number;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.baseUrl + '/User';

  constructor(private http: HttpClient) {}
  createUser(userData: any): Observable<UserResponse> {
    return this.http.post<UserResponse>(this.apiUrl, userData);
  }
  inactivateUser(userId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${userId}`);
  }
  updateUser(userData: any): Observable<any> {
    return this.http.put<any>(this.apiUrl, userData);
  }
  getUsers(): Observable<UserResponse[]> {
    return this.http.get<UserResponse[]>(this.apiUrl);
  }

}
