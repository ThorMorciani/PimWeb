import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './data-table.component.html',
  styleUrl: './data-table.component.scss'
})
export class DataTableComponent implements OnChanges {
  @Input() data: any[] = [];
  @Input() page: number = 1;
  @Output() pageChange = new EventEmitter<number>();
  @Output() action = new EventEmitter<{type: string, row: any}>();

  headers: string[] = [];

  ngOnChanges() {
    if (this.data.length > 0) {
      this.headers = Object.keys(this.data[0]); // Pega as chaves do primeiro objeto
    }
  }

  changePage(newPage: number) {
    this.pageChange.emit(newPage);
  }

  onAction(type: string, row: any) {
    this.action.emit({type, row});
  }
}
