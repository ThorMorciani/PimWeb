import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface TicketResponse {
  id: number;
  description: string;
  solution?: string;
  assigneeId?: number;
  reporterId?: number;
  statusId?: number;
  rootCauseId?: number;
  createdAt?: string;
  updatedAt?: string;
  ticketNumber: string;
  rootCause: any;
  reporter: any;
  assignee: any;
}

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = environment.baseUrl + '/Ticket';

  constructor(private http: HttpClient) {}

  getTickets(): Observable<TicketResponse[]> {
    return this.http.get<TicketResponse[]>(this.apiUrl);
  }

  createTicket(ticketData: any): Observable<TicketResponse> {
    return this.http.post<TicketResponse>(this.apiUrl, ticketData);
  }

  updateTicket(ticketData: any): Observable<any> {
    return this.http.put<any>(this.apiUrl, ticketData);
  }

  deleteTicket(ticketId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${ticketId}`);
  }
}
