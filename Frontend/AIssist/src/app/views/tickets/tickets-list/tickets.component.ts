import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { TicketService, TicketResponse } from '../../../../core/services/ticket/ticket.service';
import { ButtonComponent } from '../../../components/button/button.component';
import { Router } from '@angular/router';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { FormatDatePipe } from '../../../shared/pipes/formatDatePipe.pipe';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatIconModule, MatDialogModule, MatTableModule,
            FormatDatePipe],
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {
onDelete(_t76: any) {
throw new Error('Method not implemented.');
}
onEdit(_t76: any) {
throw new Error('Method not implemented.');
}
  tickets: TicketResponse[] = [];
  totalTickets = 0;
  paginaAtual = 1;
  totalPorPagina = 15;
  displayedColumns: string[] = ['Number','Description','Assignee','Status', 'UpdatedAt', 'acoes'];
  dataSource = new MatTableDataSource<TicketResponse>();

  constructor(
    private ticketService: TicketService, 
    private dialog: MatDialog,
    private router: Router
  ) {}

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit() {
    this.carregarTickets();
  }

  irParaNovoTicket() {
    this.router.navigate(['/tickets/novo']);
  }

  carregarTickets() {
    this.ticketService.getTickets().subscribe({
      next: (res) => {
        this.totalTickets = res.length;
        this.dataSource = new MatTableDataSource(res);

        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.ticketNumber,
            data.description,
            data.solution,
            data.status,
            new Date(data.updatedAt).toLocaleDateString('pt-BR')
          ];
  
          return valuesToSearch.some(value =>
            value?.toString().toLowerCase().includes(filterValue)
          );
        };
      },
      error: (err) => console.error('Erro ao buscar tickets:', err)
    });
  }

  deleteTicket(ticketId: number) {
    if (confirm('Tem certeza que deseja excluir este ticket?')) {
      this.ticketService.deleteTicket(ticketId).subscribe({
        next: () => {
          this.tickets = this.tickets.filter(t => t.id !== ticketId);
        },
        error: err => {
          console.error('Erro ao excluir ticket', err);
        }
      });
    }
  }

  private formatarDataLocal(data: string): string {
    const d = new Date(data);
    const dia = String(d.getDate()).padStart(2, '0');
    const mes = String(d.getMonth() + 1).padStart(2, '0');
    const ano = d.getFullYear();
    const horas = String(d.getHours()).padStart(2, '0');
    const minutos = String(d.getMinutes()).padStart(2, '0');
    return `${dia}/${mes}/${ano} ${horas}:${minutos}`;
  }

  carregarPagina(p: number) {
    this.paginaAtual = p;
    this.carregarTickets();
  }
}
