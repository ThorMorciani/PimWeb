import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  // 🔹 Propriedades que aparecem no HTML
  userName: string = 'Aluno Unip';
  userEmail: string = 'aluno.unip@gmail.com';

  // 🔹 Texto de boas-vindas (poderia vir de um serviço futuramente)
  welcomeMessage: string = 'Olá, seja bem-vindo!';
  description: string = 'Precisa de ajuda com algo? Abra um ticket agora e nossa equipe irá analisar sua solicitação com atenção para encontrar a melhor solução para você. Estamos aqui para ajudar!';

  // 🔹 Nova frase a ser exibida
  customMessage: string = `Nome do aluno: ${this.userName} | E-mail: ${this.userEmail}`;

  // 🔹 Método para o botão "Explorar!"
  explorar(): void {
    console.log('Botão Explorar clicado!');
    alert('Você clicou em Explorar!');
  }

  // 🔹 Método de Logout (exemplo)
  logout(): void {
    console.log('Logout realizado!');
    alert('Você saiu do sistema.');
  }
}
