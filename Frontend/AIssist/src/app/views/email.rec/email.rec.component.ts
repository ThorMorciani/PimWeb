import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-email-rec',
  standalone: true,
  imports: [ReactiveFormsModule], // 
  templateUrl: './email.rec.component.html',
  styleUrls: ['./email.rec.component.scss']
})
export class EmailRecComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  email() {
    if (this.form.valid) {
      console.log('Enviando email:', this.form.value.email);
    } else {
      console.log('Form inválido');
    }
  }
}
