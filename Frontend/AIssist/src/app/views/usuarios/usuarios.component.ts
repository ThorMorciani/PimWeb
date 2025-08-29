import { Component } from '@angular/core';
import { ButtonComponent } from '../../components/button/button.component';
import { DataTableComponent} from '../../components/data-table/data-table.component';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [ButtonComponent, DataTableComponent],
  templateUrl: './usuarios.component.html',
  styleUrl: './usuarios.component.scss'
})
export class UsuariosComponent {
  usuarios = [
    { Usuario: 'Murilo Câmara', Cargo: 'Administrador', 'Último login': '25/03/2025 20:00', Status: 'Online', 'Criado em': '25/03/2025 20:00' },
    { Usuario: 'Murilo Câmara', Cargo: 'Coordenador', 'Último login': '25/03/2025 20:00', Status: 'Offline', 'Criado em': '25/03/2025 20:00' },
    // ...dados da API
  ];

paginaAtual = 1;

carregarPagina(p: number) {
  this.paginaAtual = p;
  // chamada API passando p
}

executarAcao(event: any) {
  console.log('Ação:', event.type, 'Na linha:', event.row);
}

}
