import { Component, EventEmitter, Input, Output, OnChanges, Inject, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UserService, UserResponse } from '../../../core/services/user/user.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-user-edit-dialog',
  templateUrl: './user-edit-dialog.component.html',
  styleUrls: ['./user-edit-dialog.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ]
})
export class UserEditDialogComponent {
  showModal = false;
  showOkButton = false;
  modalMessage = '';
  modalTitleMessage = '';
  form: FormGroup;
  @Input() aberto = false;
  @Input() user: UserResponse | undefined;
  @Output() fechado = new EventEmitter<void>();
  constructor(
    private fb: FormBuilder,
    private userService: UserService
  ) {
    this.form = this.fb.group({
      username: [this.user?.username || '', [Validators.required]],
      name: [this.user?.name || '', [Validators.required]],
      email: [this.user?.email || '', [Validators.required, Validators.email]]
    });
  }

  confirmar() {
    if (this.form.invalid) return;

    const updatedUser = { ...this.user, ...this.form.value };
    if (!updatedUser.password) delete updatedUser.password;

    this.userService.updateUser(updatedUser).subscribe({
      next: () => {
        this.modalMessage = 'Usuário atualizado com sucesso.';
        this.modalTitleMessage = 'Sucesso';
        this.showOkButton = true;
        this.showModal = true;
      },
      error: err => {
        this.modalMessage = 'Erro ao atualizar usuário.';
        this.modalTitleMessage = 'Erro';
        this.showOkButton = true;
        this.showModal = true;
      }
    });
  }

  cancelar() {
    this.fechado.emit();
  }
}