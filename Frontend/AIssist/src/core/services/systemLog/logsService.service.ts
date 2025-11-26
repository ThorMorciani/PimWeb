import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LogResponse {
  action: string;
  description: string;
  createdAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class LogsService {
  private apiUrl = environment.baseUrl + '/Logs';

  constructor(private http: HttpClient) {}
  getLogs(): Observable<LogResponse[]> {
    return this.http.get<LogResponse[]>(this.apiUrl);
  }
}
