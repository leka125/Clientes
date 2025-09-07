import { Component, Input } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { LoadingSpinnerComponent } from '../loading-spinner/loading-spinner.component';
import { Cliente } from '../../models/cliente.model';

@Component({
  selector: 'app-cliente-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    LoadingSpinnerComponent,
    DatePipe
  ],
  templateUrl: './cliente-detail.component.html',
  styleUrls: ['./cliente-detail.component.scss']
})
export class ClienteDetailComponent {
  @Input() cliente?: Cliente;
  @Input() loading = false;
  @Input() error?: string;
}