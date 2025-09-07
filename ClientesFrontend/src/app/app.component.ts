import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

// Angular Material Modules
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

// Componentes
import { ClienteFormComponent } from './components/cliente-form/cliente-form.component';
import { ClienteDetailComponent } from './components/cliente-detail/cliente-detail.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';

// Servicios
import { ClienteService } from './services/cliente.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    ReactiveFormsModule,
    HttpClientModule,
    
    // Angular Material
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    
    // Componentes
    ClienteFormComponent,
    ClienteDetailComponent,
    LoadingSpinnerComponent
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
  providers: [ClienteService]
})
export class AppComponent {
  cliente?: any;
  loading = false;
  error?: string;

  constructor(private clienteService: ClienteService) {}

  onBuscarCliente(identificacion: string): void {
    this.loading = true;
    this.error = undefined;
    this.cliente = undefined;

    this.clienteService.buscarClientePorIdentificacion(identificacion)
      .subscribe({
        next: (cliente) => {
          this.cliente = cliente;
          this.loading = false;
        },
        error: (error) => {
          this.error = error.message;
          this.loading = false;
        }
      });
  }
}