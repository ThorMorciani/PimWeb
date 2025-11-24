import { CommonModule, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TicketService, TicketResponse } from '../../../../core/services/ticket/ticket.service';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { UserService } from '../../../../core/services/user/user.service';
import { ButtonComponent } from '../../../components/button/button.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-visualizar-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonComponent, MatIconModule],
  templateUrl: './visualizar-ticket.component.html',
  styleUrls: ['./visualizar-ticket.component.scss']
})
export class VisualizarTicketComponent implements OnInit {

  ticket!: TicketResponse;
  usuarioAtual: any;
  cargo = '';
  tecnicos: any[] = [];

  assigneeSelecionado: number | null = null;
  carregando = false;

  solution: string = '';
  editarResponsavel: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService,
    private authService: AuthService,
    private userService: UserService,
    private location: Location
  ) {}

  ngOnInit() {
    this.usuarioAtual = this.authService.getUsuarioAtual();
    this.cargo = this.usuarioAtual?.profile ?? '';

    const ticketNumber = this.route.snapshot.paramMap.get('ticketNumber');

    if (ticketNumber) {
      this.carregarTicket(ticketNumber);
    }

    this.carregarTecnicos();
  }

  carregarTicket(ticketNumber: string) {
    this.ticketService.getTicketByTicketNumber(ticketNumber).subscribe({
      next: (res) => {
        this.ticket = res;
        this.solution = res.solution ?? '';
      },
      error: (err) => console.error('Erro ao carregar ticket:', err)
    });
  }

  carregarTecnicos() {
    this.userService.getTechnicians().subscribe({
      next: lista => this.tecnicos = lista,
      error: () => alert('Erro ao carregar técnicos.')
    });
  }

  obterClasseStatus(statusId: number | undefined) {
    if (!statusId) return 'open';
    switch (statusId) {
      case 1: return 'open';
      case 2: return 'assigned';
      case 3: return 'in-progress';
      case 4: return 'waiting';
      case 5: return 'resolved';
      case 6: return 'canceled';
      default: return 'open';
    }
  }

  obterTextoStatus(statusId: number | undefined) {
    const map: any = {
      1: 'Aberto',
      2: 'Atribuído',
      3: 'Em Atendimento',
      4: 'Em Validação',
      5: 'Fechado',
      6: 'Cancelado'
    };
    return map[statusId ?? 1] || 'Desconhecido';
  }

  voltar() {
    this.location.back();
  }

  isAdminOrManager(): boolean {
    return this.usuarioAtual?.profile === 'Administrador' 
        || this.usuarioAtual?.profile === 'Gerente';
  }

  assumirTicket() {
    if (!this.ticket) return;

    const updateData = {
      ...this.ticket,
      assigneeId: this.usuarioAtual?.id
    };

    this.carregando = true;

    this.ticketService.updateTicket(updateData).subscribe({
      next: () => {
        alert('Ticket assumido com sucesso!');
        this.ticket.assignee = { name: this.usuarioAtual.name };
        this.carregando = false;
      },
      error: () => {
        alert('Erro ao assumir ticket');
        this.carregando = false;
      }
    });
  }

  definirResponsavel() {
    if (!this.assigneeSelecionado) {
      alert('Selecione um técnico!');
      return;
    }

    const body = {
      ticketNumber: this.ticket.ticketNumber,
      assigneeId: this.assigneeSelecionado
    };

    this.ticketService.updateAssignee(body).subscribe({
      next: () => {
        alert('Responsável atualizado com sucesso!');
        this.editarResponsavel = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => alert('Erro ao atualizar responsável')
    });
  }

  salvarTicket() {

    const updateData = {
      ticketNumber: this.ticket.ticketNumber,
      description: this.ticket.description,
      solution: this.solution
    };

    this.ticketService.updateTicket(updateData).subscribe({
      next: () => alert('Descrição salva com sucesso!'),
      error: () => alert('Erro ao salvar descrição.')
    });
  }

  getUserInitials(name?: string): string {
    if (!name) return '';
    const names = name.trim().split(' ');
    const firstInitial = names[0].charAt(0);
    const lastInitial = names.length > 1 ? names[names.length - 1].charAt(0) : '';
    return (firstInitial + lastInitial).toUpperCase();
  }
}
