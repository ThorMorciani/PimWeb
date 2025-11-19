import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RootCauseResponse {
  id: number;
  rootCauseName: string;
  criticality: number;
  updatedAt: string;
  active: number;
}

@Injectable({
  providedIn: 'root'
})
export class RootCauseService {
  private apiUrl = environment.baseUrl + '/RootCause';

  constructor(private http: HttpClient) {}
  inactivateRootCause(rootCauseId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${rootCauseId}`);
  }
  getRootCauses(): Observable<RootCauseResponse[]> {
    return this.http.get<RootCauseResponse[]>(this.apiUrl);
  }
}
