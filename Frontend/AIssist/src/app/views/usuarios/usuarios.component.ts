import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService, UserResponse } from '../../../core/services/user/user.service';
import { ButtonComponent } from '../../components/button/button.component';
import { UserEditDialogComponent } from '../../components/user-edit-dialog/user-edit-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';
import { ModalUserComponent } from './modal-user/modal-user.component';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { booleanToStringPipe } from '../../shared/pipes/booleanToString.pipe';
import { FormatDatePipe } from '../../shared/pipes/formatDatePipe.pipe';
import { EnumTextMap } from '../../shared/enums/enum-maps';
import { EnumTextPipe } from '../../shared/pipes/enumToText.pipe';

interface Usuario extends UserResponse {
  created_At?: string;
  updated_At?: string;
}

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatTableModule, MatIconModule, MatDialogModule, 
            UserEditDialogComponent, MatTooltipModule, ModalUserComponent,
            booleanToStringPipe, FormatDatePipe, EnumTextPipe],
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  totalUsuarios = 0;
  paginaAtual = 1;
  totalPorPagina = 15;
  modalAberto = false;
  usuarioAtual: UserResponse | undefined;
  @ViewChild('modalUser') modalUser!: ModalUserComponent;
  displayedColumns: string[] = ['Name','Username','Email','Profile', 'Active','CreatedAt', 'UpdatedAt', 'acoes'];
  dataSource = new MatTableDataSource<UserResponse>();

  constructor(
    private userService: UserService,
    private dialog: MatDialog
  ) {}

  onEdit(_t49: any) {
    throw new Error('Method not implemented.');
    }
    onDelete(_t49: any) {
    throw new Error('Method not implemented.');
    }
  ngOnInit() {
    this.carregarUsuarios();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  carregarUsuarios() {
    this.userService.getUsers().subscribe({
      next: (res: UserResponse[]) => {
        this.totalUsuarios = res.length;
        this.dataSource = new MatTableDataSource(res);

        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.name,
            data.username,
            data.profile,
            data.active ? 'Ativo' : 'Inativo',
            new Date(data.updatedAt).toLocaleDateString('pt-BR')
          ];
  
          return valuesToSearch.some(value =>
            value?.toString().toLowerCase().includes(filterValue)
          );
        };
      },
      error: (err) => console.error('Erro ao buscar usuários:', err)
    });
  }

  deleteUser(userId: number) {
    if (confirm('Tem certeza que deseja inativar este usuário?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.usuarios = this.usuarios.filter(u => u.id !== userId);
        },
        error: err => {
          console.error('Erro ao inativar usuário', err);
        }
      });
    }
  }

  createUser() {
    this.modalUser.formData = null;
    this.modalUser.open();
  }

  openEditDialog(user: UserResponse) {
    
    this.usuarioAtual = user;
    this.modalUser.open();
  }

  formatarDataLocal(data: string): string {
    const d = new Date(data);
    const dia = String(d.getDate()).padStart(2,'0');
    const mes = String(d.getMonth()+1).padStart(2,'0');
    const ano = d.getFullYear();
    const horas = String(d.getHours()).padStart(2,'0');
    const minutos = String(d.getMinutes()).padStart(2,'0');
    return `${dia}/${mes}/${ano} ${horas}:${minutos}`;
  }

  getProfileName(id: number): string {
    const profiles: { [key: number]: string } = {
      1: 'Administrador',
      2: 'Gerente',
      3: 'Usuário',
      4: 'Visitante'
    };
    return profiles[id] || 'Desconhecido';
  }

  executarAcao(event: { type: string; row: Usuario }) {
    switch(event.type) {
      case 'edit': this.openEditDialog(event.row); break;
      case 'delete': this.deleteUser(event.row.id); break;
      case 'toggle': console.log('Alternar status de:', event.row); break;
      default: console.warn('Ação desconhecida:', event.type);
    }
  }

  carregarPagina(p: number) {
    this.paginaAtual = p;
    this.carregarUsuarios();
  }

  fecharModal(): void {
    this.modalAberto = false;
  }

  onFormSubmit(event: { formData: any; confirmed: boolean }) {
    if (!event.confirmed) return;

    this.userService.createUser({
      name: event.formData.name,
      username: event.formData.username,
      password: event.formData.password,
      email: event.formData.email,
      profileId: event.formData.profileId
    })
    .subscribe({
      next: res => {
        console.log('Usuário criado:', res);
        this.carregarUsuarios();
      },
      error: err => {
        console.error('Erro ao criar usuário:', err);
      }
    });
  }

}
