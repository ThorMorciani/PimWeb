import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
  standalone: true,
})
export class ButtonComponent {
  @Input() texto: string = 'Botão';
  @Input() setDanger: boolean = false;
  @Input() block: boolean = false;
  @Input() active: boolean = true;
  @Input() outlined: boolean = false;
  @Input() size: 'small' | 'large' = 'large';
  @Input() type: 'primary' | 'secondary' | 'tertiary' = 'primary';

  get buttonClasses(): string {
    const classes = [
      `${this.type}-${this.size}-button`,
      this.setDanger ? 'danger' : '',
      this.block ? 'block' : '',
      !this.active ? 'disabled' : '',
      this.outlined ? 'outlined' : ''
    ];
    return classes.join(' ');
  }
}
