import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cod-recuperacao',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cod-recuperacao.component.html',
  styleUrls: ['./cod-recuperacao.component.scss']
})
export class CodRecuperacaoComponent {
recuperacao() {
throw new Error('Method not implemented.');
}
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      codigo: ['']
    });
  }

  confirmar() {
    console.log(this.form.value);
  }
}
