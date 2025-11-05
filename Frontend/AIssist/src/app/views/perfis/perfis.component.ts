import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ButtonComponent } from '../../components/button/button.component';
import { ProfileResponse, ProfileService } from '../../../core/services/profile/profile.service';
import { FormatDatePipe } from '../../shared/pipes/formatDatePipe.pipe';
import { booleanToStringPipe } from '../../shared/pipes/booleanToString.pipe';

@Component({
  selector: 'app-perfis',
  standalone: true,
  imports: [CommonModule, ButtonComponent, MatTableModule,
    MatIconModule, MatDialogModule, MatTooltipModule, booleanToStringPipe, FormatDatePipe],
  templateUrl: './perfis.component.html',
  styleUrl: './perfis.component.scss'
})
export class PerfisComponent implements OnInit{
onEdit(_t49: any) {
throw new Error('Method not implemented.');
}
onDelete(_t49: any) {
throw new Error('Method not implemented.');
}
  displayedColumns: string[] = ['profileName', 'Active', 'UpdatedAt', 'acoes'];
  dataSource = new MatTableDataSource<ProfileResponse>();

  constructor(private profileService: ProfileService) {}

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngOnInit(): void {
    this.profileService.getProfiles().subscribe({
      next: (resp) => {
        this.dataSource = new MatTableDataSource(resp);

        this.dataSource.filterPredicate = (data: any, filter: string): boolean => {
          const filterValue = filter.trim().toLowerCase();
          const valuesToSearch = [
            data.profileName,
            data.active ? 'Ativo' : 'Inativo',
            new Date(data.updatedAt).toLocaleDateString('pt-BR')
          ];
  
          return valuesToSearch.some(value =>
            value?.toString().toLowerCase().includes(filterValue)
          );
        };
      } 
    });
  }
}
