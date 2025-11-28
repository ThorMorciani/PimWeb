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
  statusSelecionado: number | null = null;

  carregando = false;
  solution: string = '';
  editarResponsavel = false;
  editarStatus = false;

  listaStatus = [
    { id: 1, nome: 'Aberto' },
    { id: 2, nome: 'Atribuído' },
    { id: 3, nome: 'Em Atendimento' },
    { id: 4, nome: 'Em Validação' },
    { id: 5, nome: 'Fechado' },
    { id: 6, nome: 'Cancelado' }
  ];

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
        this.statusSelecionado = res.statusId ?? null;
        this.assigneeSelecionado = res.assignee?.id ?? null;
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
    return this.usuarioAtual?.profile === 'administrador' 
        || this.usuarioAtual?.profile === 'gerente';
  }

  isUser():boolean {
    return this.usuarioAtual?.profile === 'usuario'
  }

  assumirTicket() {
    if (!this.ticket) return;

    const updateData = {
      ticketNumber: this.ticket.ticketNumber,
      assigneeId: this.usuarioAtual?.id
    };

    this.carregando = true;

    this.ticketService.updateAssignee(updateData).subscribe({
      next: () => {
        alert('Ticket assumido com sucesso!');
        this.editarResponsavel = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => alert('Erro ao assumir ticket')
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

  definirStatus() {
    if (!this.statusSelecionado) {
      alert('Selecione um status!');
      return;
    }

    const body = {
      ticketNumber: this.ticket.ticketNumber,
      status: this.statusSelecionado
    };

    this.ticketService.updateTicketStatus(body).subscribe({
      next: () => {
        alert('Status atualizado com sucesso!');
        this.editarStatus = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => alert('Erro ao atualizar status.')
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
