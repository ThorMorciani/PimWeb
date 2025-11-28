import { CommonModule, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TicketService, TicketResponse } from '../../../../core/services/ticket/ticket.service';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { UserService } from '../../../../core/services/user/user.service';
import { ButtonComponent } from '../../../components/button/button.component';
import { MatIconModule } from '@angular/material/icon';
import { ConfirmModalComponent } from '../../../shared/components/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-visualizar-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonComponent, MatIconModule, ConfirmModalComponent],
  templateUrl: './visualizar-ticket.component.html',
  styleUrls: ['./visualizar-ticket.component.scss']
})
export class VisualizarTicketComponent implements OnInit {
  ticket!: TicketResponse;
  usuarioAtual: any;
  cargo = '';
  tecnicos: any[] = [];
  showModal = false;
  showOkButton = false;
  modalMessage = '';
  modalTitleMessage = '';

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
      error: (err) => {
        this.modalMessage = 'Falha ao listar tickets.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  carregarTecnicos() {
    this.userService.getTechnicians().subscribe({
      next: lista => this.tecnicos = lista,
      error: () => {
        this.modalMessage = 'Falha ao listar técnicos.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
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
        this.modalMessage = 'Ticket atribuído com sucesso.';
        this.modalTitleMessage = 'Sucesso';
        this.showOkButton = true;
        this.showModal = true;
        this.editarResponsavel = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => {
        this.modalMessage = 'Falha ao atribuir ticket.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  definirResponsavel() {
    if (!this.assigneeSelecionado) {
      this.modalMessage = 'Selecione um técnico.';
      this.modalTitleMessage = 'Erro';
      this.showOkButton = true;
      this.showModal = true;
      return;
    }

    const body = {
      ticketNumber: this.ticket.ticketNumber,
      assigneeId: this.assigneeSelecionado
    };

    this.ticketService.updateAssignee(body).subscribe({
      next: () => {
        this.modalMessage = 'Responsável atualizado com sucesso.';
        this.modalTitleMessage = 'Sucesso';
        this.showOkButton = true;
        this.showModal = true;
        this.editarResponsavel = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => {
        this.modalMessage = 'Erro ao atualizar responsável.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  definirStatus() {
    if (!this.statusSelecionado) {
      this.modalMessage = 'Selecione um técnico.';
      this.modalTitleMessage = 'Erro';
      this.showOkButton = true;
      this.showModal = true;
      return;
    }

    const body = {
      ticketNumber: this.ticket.ticketNumber,
      status: this.statusSelecionado
    };

    this.ticketService.updateTicketStatus(body).subscribe({
      next: () => {
        this.modalMessage = 'Status atualizado com sucesso.';
        this.modalTitleMessage = 'Sucesso';
        this.showOkButton = true;
        this.showModal = true;
        this.editarStatus = false;
        this.carregarTicket(this.ticket.ticketNumber);
      },
      error: () => {
        this.modalMessage = 'Erro ao atualizar status.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  salvarTicket() {
    const updateData = {
      ticketNumber: this.ticket.ticketNumber,
      description: this.ticket.description,
      solution: this.solution
    };

    this.ticketService.updateTicket(updateData).subscribe({
      next: () => {
        this.modalMessage = 'Descrição salva com sucesso.';
        this.modalTitleMessage = 'Sucesso';
        this.showOkButton = true;
        this.showModal = true;
      },
      error: () => {
        this.modalMessage = 'Erro ao salvar descrição.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  getUserInitials(name?: string): string {
    if (!name) return '';
    const names = name.trim().split(' ');
    const firstInitial = names[0].charAt(0);
    const lastInitial = names.length > 1 ? names[names.length - 1].charAt(0) : '';
    return (firstInitial + lastInitial).toUpperCase();
  }

  onCancelAction() {
    this.showModal = false;
    this.showOkButton = false;
  }
}
