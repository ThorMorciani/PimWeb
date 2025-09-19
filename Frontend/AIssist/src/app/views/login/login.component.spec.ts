import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { delay, Observable, of } from 'rxjs';

export interface CreateUserDTO {
  username: string;
  name: string;
  email: string;
  password: string;
}

export interface User {
  id: string;
  username: string|null;
  name: string;
  email: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl = '/api/users'; 

  constructor(private http: HttpClient) {}

  create(body: CreateUserDTO): Observable<User> {
    const mock: User = {
      id: crypto.randomUUID(),
      username: body.username,
      name: body.name,
      email: body.email,
      createdAt: new Date().toISOString(),
    };
    return of(mock).pipe(delay(800));
  }
}
