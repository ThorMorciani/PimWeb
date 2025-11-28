import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user/user.service';
import { TicketService } from '../../../core/services/ticket/ticket.service';
import { RootCauseService } from '../../../core/services/rootCause/root-cause.service';

import { Chart, registerables } from 'chart.js';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../components/button/button.component';

import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';

Chart.register(...registerables);

@Component({
  selector: 'app-relatorios',
  standalone: true,
  imports: [CommonModule, ButtonComponent],
  templateUrl: './relatorios.component.html',
  styleUrls: ['./relatorios.component.scss']
})
export class RelatoriosComponent implements OnInit {

  totalUsuarios = 0;
  totalTickets = 0;
  totalAssuntos = 0;

  constructor(
    private userService: UserService,
    private ticketService: TicketService,
    private assuntoService: RootCauseService
  ) {}

  ngOnInit() {
    this.carregarUsuarios();
    this.carregarTickets();
    this.carregarAssuntos();
  }

  // GERAR DIAS DO MÊS
  gerarDiasMes(): string[] {
    const hoje = new Date();
    const ano = hoje.getFullYear();
    const mes = hoje.getMonth() + 1;

    const ultimoDia = new Date(ano, mes, 0).getDate();
    const dias: string[] = [];

    for (let d = 1; d <= ultimoDia; d++) {
      const dia = d.toString().padStart(2, '0');
      const mesStr = mes.toString().padStart(2, '0');
      dias.push(`${dia}/${mesStr}`);
    }

    return dias;
  }

  // USUÁRIOS
  carregarUsuarios() {
    this.userService.getUsers().subscribe({
      next: (usuarios) => {
        this.totalUsuarios = usuarios.length;
        this.criarGraficoUsuariosPorDia(usuarios);
      },
      error: err => console.error(err)
    });
  }

  criarGraficoUsuariosPorDia(usuarios: any[]) {
  const diasMes = this.gerarDiasMes();

  const mapa: Record<string, number> = diasMes.reduce((acc, dia) => {
    acc[dia] = 0;
    return acc;
  }, {} as Record<string, number>);

  usuarios.forEach(u => {
    const data = new Date(u.createdAt);
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const chave = `${dia}/${mes}`;

    if (mapa[chave] !== undefined) {
      mapa[chave]++;
    }
  });

  new Chart('usuariosPorDia', {
    type: 'bar',
    data: {
      labels: diasMes,
      datasets: [{
        label: 'Usuários criados por dia',
        data: Object.values(mapa),
        backgroundColor: '#055DFC'
      }]
    },
    options: {
      responsive: true,
      devicePixelRatio: 2
    }
  });
}


  // TICKETS
  carregarTickets() {
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        this.totalTickets = tickets.length;

        this.criarGraficoAbertosFechados(tickets);
        this.criarGraficoTicketsPorDia(tickets);
        this.criarGraficoTicketsPorAssunto(tickets);
      }
    });
  }

  criarGraficoAbertosFechados(tickets: any[]) {
    const abertos = tickets.filter(t => t.status !== "Fechado" && t.status !== "Cancelado").length;
    const fechados = tickets.filter(t => t.status === "Fechado" || t.status === "Cancelado").length;

    new Chart('ticketsAbertosFechados', {
      type: 'doughnut',
      data: {
        labels: ['Abertos', 'Fechados'],
        datasets: [{
          data: [abertos, fechados],
          backgroundColor: ['#FF3D00', '#2ECC71']
        }]
      }
    });
  }

  criarGraficoTicketsPorDia(tickets: any[]) {
    const diasMes = this.gerarDiasMes();

    const mapa: Record<string, number> = diasMes.reduce((acc, dia) => {
      acc[dia] = 0;
      return acc;
    }, {} as Record<string, number>);

    tickets.forEach(t => {
      const data = new Date(t.createdAt);
      const dia = data.getDate().toString().padStart(2, '0');
      const mes = (data.getMonth() + 1).toString().padStart(2, '0');
      const chave = `${dia}/${mes}`;

      if (mapa[chave] !== undefined) {
        mapa[chave]++;
      }
    });

    new Chart('ticketsPorDia', {
      type: 'bar',
      data: {
        labels: diasMes,
        datasets: [{
          label: 'Tickets criados por dia',
          data: Object.values(mapa),
          backgroundColor: '#055DFC'
        }]
      },
      options: {
        responsive: true,
        devicePixelRatio: 2
      }
    });
  }

  // ASSUNTOS
  carregarAssuntos() {
    this.assuntoService.getRootCauses().subscribe({
      next: (assuntos) => this.totalAssuntos = assuntos.length
    });
  }

  criarGraficoTicketsPorAssunto(tickets: any[]) {
    const porCausa: Record<string, number> = {};

    tickets.forEach(t => {
      const nome = t.rootCause?.rootCauseName || 'Não informado';
      porCausa[nome] = (porCausa[nome] || 0) + 1;
    });

    new Chart('ticketsPorAssunto', {
      type: 'pie',
      data: {
        labels: Object.keys(porCausa),
        datasets: [{
          data: Object.values(porCausa),
          backgroundColor: [
            '#055DFC', '#2ECC71', '#FF3D00',
            '#F1C40F', '#8E44AD', '#16A085'
          ]
        }]
      }
    });
  }

  exportarPDF() {
    const element = document.querySelector('.container-dashboard') as HTMLElement;

    if (!element) return;

    html2canvas(element, { scale: 2, useCORS: true })
      .then(canvas => {
        const imgData = canvas.toDataURL('image/png', 1.0);

        const pdf = new jsPDF('p', 'mm', 'a4');

        const pageWidth = 210;
        const pageHeight = 295;

        const imgWidth = pageWidth;
        const imgHeight = (canvas.height * imgWidth) / canvas.width;

        let heightLeft = imgHeight;
        let position = 0;
        
        pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;

        while (heightLeft > 0) {
          position = heightLeft - imgHeight;

          pdf.addPage();
          pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);

          heightLeft -= pageHeight;
        }

        pdf.save('relatorio.pdf');
      });
  }
}
