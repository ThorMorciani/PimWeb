import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { FormatDatePipe } from '../../shared/pipes/formatDatePipe.pipe';
import { LogResponse, LogsService } from '../../../core/services/systemLog/logsService.service';

@Component({
  selector: 'app-logs',
  standalone: true,
  imports: [MatTableModule, CommonModule, MatIconModule, FormatDatePipe],
  templateUrl: './system-logs.component.html',
  styleUrl: './system-logs.component.scss'
})
export class SystemLogsComponent implements OnInit{
  displayedColumns: string[] = ['action','description', 'createdAt'];
  dataSource = new MatTableDataSource<LogResponse>();


  constructor(private logsService: LogsService) {}
  ngOnInit(): void {
    this.loadLogs();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  loadLogs() {
    this.logsService.getLogs().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.action,
            data.description,
            new Date(data.createdAt).toLocaleDateString('pt-BR')
          ];
          return valuesToSearch.some(value =>
            value?.toString().toLowerCase().includes(filterValue)
          );
        };
      }
    });
  }
}
