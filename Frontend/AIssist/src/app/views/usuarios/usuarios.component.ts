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
import { ConfirmModalComponent } from '../../shared/components/confirm-modal/confirm-modal.component';

interface Usuario extends UserResponse {
  created_At?: string;
  updated_At?: string;
}

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatTableModule, MatIconModule, MatDialogModule, 
            MatTooltipModule, ModalUserComponent, booleanToStringPipe, FormatDatePipe, EnumTextPipe,
            ConfirmModalComponent],
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent implements OnInit {
  showModal = false;
  showOkButton = false;
  textMessage = '';
  usuarios: Usuario[] = [];
  totalUsuarios = 0;
  paginaAtual = 1;
  totalPorPagina = 15;
  modalAberto = false;
  selectedItem = null;
  usuarioAtual: UserResponse | undefined;
  @ViewChild('modalUser') modalUser!: ModalUserComponent;
  displayedColumns: string[] = ['Name','Username','Email','Profile', 'Active','CreatedAt', 'UpdatedAt', 'acoes'];
  dataSource = new MatTableDataSource<UserResponse>();

  constructor(
    private userService: UserService,
    private dialog: MatDialog
  ) {}
  openConfirmationModal(item: any, message: string, showOkButton: boolean = false) {
    this.textMessage = message;
    this.selectedItem = item;
    this.showModal = true;
  }
  
  onConfirmAction() {
    this.inactivateItem(this.selectedItem);
    this.showModal = false;
  }
  
  onCancelAction() {
    this.selectedItem = null;
    this.showModal = false;
    this.showOkButton = false;
  }
  ngOnInit() {
    this.loadUsers();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  loadUsers() {
    this.userService.getUsers().subscribe({
      next: (res: UserResponse[]) => {
        this.totalUsuarios = res.length;
        this.dataSource = new MatTableDataSource(res);

        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.name,
            data.username,
            data.profile.profileName,
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

  inactivateItem(element: any) {
    this.userService.inactivateUser(element.id).subscribe({
      next: (resp) => {
        this.loadUsers();
      },
      error: (err) => {
        console.log(err)
      }
    });
  }

  openUserModal(isEdit: boolean, element: any = null) {
    this.selectedItem = null;
    if (isEdit)
      this.selectedItem = element;

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

  carregarPagina(p: number) {
    this.paginaAtual = p;
    this.loadUsers();
  }

  fecharModal(): void {
    this.modalAberto = false;
  }

  onFormSubmit(event: { formData: any, isEdit: boolean }) {
    let user: any;
    if (event.isEdit) {
      user = {
        id: event.formData.id,
        name: event.formData.name,
        username: event.formData.username,
        email: event.formData.email,
        profileId: event.formData.profileId
      };
      this.editUser(user);
    } else {
      user = {
        name: event.formData.name,
        username: event.formData.username,
        email: event.formData.email,
        profileId: event.formData.profileId,
        password: event.formData.password
      };
      this.createUser(user);
    }
  }

  createUser(user: any) {
    this.userService.createUser(user).subscribe({
      next: (resp) => {
        this.loadUsers();
        this.showOkButton = true;
        this.showModal = true;
      },
      error: (err) => {
        this.loadUsers();
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }
  editUser(user: any) {
    this.userService.updateUser(user).subscribe({
      next: (resp) => {
        this.loadUsers();
        this.showOkButton = true;
        this.textMessage = "Usuário editado com sucesso."
        this.showModal = true;
      },
      error: (err) => {
        this.loadUsers();
        this.showOkButton = true;
        this.textMessage = "Erro ao editar usuário."
        this.showModal = true;
      }
    });
  }
}
