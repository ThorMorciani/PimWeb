import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'booleanToString',
  standalone: true
})
export class booleanToStringPipe implements PipeTransform {
  transform(value: boolean): string {
    return value ? 'Ativo' : 'Inativo';
  }
}