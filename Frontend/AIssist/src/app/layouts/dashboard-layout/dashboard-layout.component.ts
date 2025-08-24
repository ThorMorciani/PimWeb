import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '../../components/sidebar/sidebar.component';

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [RouterModule, SidebarComponent], // corrigiremos a importação abaixo
  templateUrl: './dashboard-layout.component.html', // ✅ TEMPLATE CORRETO
  styleUrls: ['./dashboard-layout.component.scss']   // ou .css
})
export class DashboardLayoutComponent {} // ✅ exportar a classe