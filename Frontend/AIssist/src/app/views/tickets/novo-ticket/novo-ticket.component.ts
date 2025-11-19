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

  private converter = new showdown.Converter();

  constructor(
    private ticketService: TicketService,
    private sanitizer: DomSanitizer,
    private http: HttpClient,
    private location: Location
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

    try {
      const response = await fetch(`${environment.baseUrl}/GeminiAi/suggestion`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ description: this.descricao })
      });

      const data = await response.json();
      const html = this.converter.makeHtml(data.outputText ?? '');
      this.dicasIA = this.sanitizer.bypassSecurityTrustHtml(html);
    } catch {
      this.dicasIA = this.sanitizer.bypassSecurityTrustHtml(
        'Erro ao gerar sugestão da IA.'
      );
    }
  }

  async feedbackIA(util: boolean) {
    alert(util ? "Obrigado pelo feedback!" : "Obrigado, vamos melhorar.");
    this.feedbackAtivo = false;

    try {
      const response = await fetch(`${environment.baseUrl}/GeminiAi/priority`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ description: this.descricao })
      });

      const data = await response.json();
      const prioridade = data.priority?.toLowerCase();

      this.complexidadeTicket =
        prioridade === 'alta' ? 'ALTO' :
        prioridade === 'normal' ? 'BAIXO' :
        'MÉDIO';

      this.criarTicketAtivo = true;

    } catch {
      this.complexidadeTicket = 'MÉDIO';
      this.criarTicketAtivo = true;
    }
  }

  criarTicket() {
    if (!this.assunto || !this.descricao) {
      alert('Preencha todos os campos obrigatórios!');
      return;
    }

    const ticketData = {
      description: this.descricao,
      solution: null,
      reporterId: 1,
      rootCauseId: Number(this.assunto) // ← garante TIPO NUMBER
    };

    this.ticketService.createTicket(ticketData).subscribe({
      next: () => {
        alert('Ticket criado com sucesso!');
        this.resetForm();
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
