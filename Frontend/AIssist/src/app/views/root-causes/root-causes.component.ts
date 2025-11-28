import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ButtonComponent } from '../../components/button/button.component';
import { booleanToStringPipe } from '../../shared/pipes/booleanToString.pipe';
import { EnumTextPipe } from '../../shared/pipes/enumToText.pipe';
import { FormatDatePipe } from '../../shared/pipes/formatDatePipe.pipe';
import { RootCauseResponse, RootCauseService } from '../../../core/services/rootCause/root-cause.service';
import { ConfirmModalComponent } from '../../shared/components/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-root-causes',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatTableModule, MatIconModule, MatDialogModule, 
    MatTooltipModule,booleanToStringPipe, FormatDatePipe, EnumTextPipe, ConfirmModalComponent],
  templateUrl: './root-causes.component.html',
  styleUrl: './root-causes.component.scss'
})
export class RootCausesComponent implements OnInit{
  showModal = false;
  textMessage = '';
  paginaAtual = 1;
  totalPorPagina = 15;
  selectedItem = null;
  displayedColumns: string[] = ['rootCauseName','criticality', 'active', 'updatedAt', 'acoes'];
  dataSource = new MatTableDataSource<RootCauseResponse>();

  constructor(private rootCauseService: RootCauseService) {}

  openModal(item: any, message: string) {
    this.textMessage = message;
    this.selectedItem = item;
    this.showModal = true;
  }

  onConfirmAction() {
    this.inactivateItem(this.selectedItem);
    this.showModal = false;
  }

  onCancelAction() {
    this.selectedItem = null;
    this.showModal = false;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
    this.loadRootCauses();
  }

  loadRootCauses() {
    this.rootCauseService.getRootCauses().subscribe({
      next: (resp) => {
        this.dataSource = new MatTableDataSource(resp);
        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.rootCauseName,
            data.active ? 'Ativo' : 'Inativo',
            new Date(data.updatedAt).toLocaleDateString('pt-BR')
          ];
  
          return valuesToSearch.some(value =>
            value?.toString().toLowerCase().includes(filterValue)
          );
        };
      },
      error: (err) => {
        console.log('error: ', err);
      }
    });
  }

  inactivateItem(element: any) {
    this.rootCauseService.inactivateRootCause(element.id).subscribe({
      next: (resp) => {
        this.loadRootCauses();
      },
      error: (err) => {
        console.log(err)
      }
    });
  }
}
