import { Component, Input, Output, EventEmitter } from '@angular/core';

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
  @Input() disabled: boolean = false;
  @Input() size: 'small' | 'large' = 'large';
  @Input() type: 'primary' | 'secondary' | 'tertiary' = 'primary';
  @Input() bold: boolean = false;

  @Output() click = new EventEmitter<void>();

  get buttonClasses(): string {
    const classes = [
      `${this.type}-${this.size}-button`,
      this.setDanger ? 'danger' : '',
      this.block ? 'block' : '',
      !this.active ? 'disabled' : '',
      this.outlined ? 'outlined' : '',
      this.bold ? 'bold-button' : ''
    ];
    return classes.join(' ');
  }
  onClick() {
    if (this.active) {
      this.click.emit();
    }
  }
}
