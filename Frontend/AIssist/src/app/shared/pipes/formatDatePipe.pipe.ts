import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({
  name: 'formatDateBr',
  standalone: true
})
export class FormatDatePipe implements PipeTransform {
  transform(value: string | Date | null | undefined, showTime: boolean = true): string {
    if (!value) return '';

    const date = new Date(value);
    if (isNaN(date.getTime())) return '';

    const pattern = showTime ? 'dd/MM/yyyy HH:mm' : 'dd/MM/yyyy';
    return formatDate(date, pattern, 'pt-BR');
  }
}
