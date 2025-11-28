import { Component, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import showdown from 'showdown';
import { ButtonComponent } from '../../../components/button/button.component';
import { TicketService } from '../../../../core/services/ticket/ticket.service';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RootCause } from '../../../types/RootCauses';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-novo-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule, ButtonComponent],
  templateUrl: './novo-ticket.component.html',
  styleUrls: ['./novo-ticket.component.scss'],
})
export class NovoTicketComponent implements OnInit {

  assunto: number | null = null;
  descricao: string = '';
  dicasIA: SafeHtml = '';
  complexidadeTicket: string = '';

  iaUsada: boolean = false;
  feedbackAtivo: boolean = false;
  criarTicketAtivo: boolean = false;

  rootCauses: RootCause[] = [];

  sugestaoIA: string = '';
  iaUtil: boolean | null = null;

  private converter = new showdown.Converter();

  constructor(
    private ticketService: TicketService,
    private sanitizer: DomSanitizer,
    private http: HttpClient,
    private location: Location,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loadRootCauses();
  }

  loadRootCauses() {
    this.http.get<RootCause[]>(`${environment.baseUrl}/RootCause`)
      .subscribe({
        next: (data) => this.rootCauses = data,
        error: (err) => console.error('Erro ao carregar Root Causes:', err)
      });
  }

  onDescricaoChange() {
    this.iaUsada = false;
    this.feedbackAtivo = false;
    this.criarTicketAtivo = false;
  }

  goBack() {
    this.location.back();
  }

  async ajudaIA() {
    if (!this.descricao) {
      this.dicasIA = this.sanitizer.bypassSecurityTrustHtml(
        'Descreva o problema para receber dicas da IA.'
      );
      return;
    }

    this.dicasIA = this.sanitizer.bypassSecurityTrustHtml('Gerando sugestão...');
    this.iaUsada = true;
    this.feedbackAtivo = true;
    this.iaUtil = null;

    try {
      const response = await fetch(`${environment.baseUrl}/GeminiAi/suggestion`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ description: this.descricao })
      });

      const data = await response.json();
      this.sugestaoIA = data.outputText ?? '';
      const html = this.converter.makeHtml(this.sugestaoIA);
      this.dicasIA = this.sanitizer.bypassSecurityTrustHtml(html);
    } catch {
      this.dicasIA = this.sanitizer.bypassSecurityTrustHtml(
        'Erro ao gerar sugestão da IA.'
      );
    }
  }

  feedbackIA(util: boolean) {
    alert(util ? "Obrigado pelo feedback!" : "Obrigado, vamos melhorar.");
    this.iaUtil = util;
    this.feedbackAtivo = false;
    this.criarTicketAtivo = true;
  }

  criarTicket() {
    if (!this.assunto || !this.descricao) {
      alert('Preencha todos os campos obrigatórios!');
      return;
    }

    const usuarioLogado = this.authService.getUsuarioAtual(); 
    const reporterId = usuarioLogado?.id ?? 0;

    let status = 1;
    let solution: string | null = null;

    if (this.iaUsada && this.iaUtil !== null) {
      status = this.iaUtil ? 5 : 1;
      solution = this.iaUtil ? this.sugestaoIA : null;
    }

    const ticketData = {
      description: this.descricao,
      solution: solution ?? "Aguardando análise",
      reporterId: reporterId,
      assigneeId: null,
      rootCauseId: Number(this.assunto),
      status: status
    };

    this.ticketService.createTicket(ticketData).subscribe({
      next: () => {
        alert('Ticket criado com sucesso!');
        this.resetForm();

        this.location.back();
      },
      error: () => alert('Erro ao criar ticket')
    });    
  }

  resetForm() {
    this.assunto = null;
    this.descricao = '';
    this.dicasIA = '';
    this.iaUsada = false;
    this.feedbackAtivo = false;
    this.criarTicketAtivo = false;
    this.complexidadeTicket = '';
  }
}
