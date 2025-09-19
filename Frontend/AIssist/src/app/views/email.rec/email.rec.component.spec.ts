import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-email.rec',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './email.rec.component.html',
  styleUrl: './email.rec.component.scss'
})
export class EmailRecComponent {
form: any;
email: any;

}