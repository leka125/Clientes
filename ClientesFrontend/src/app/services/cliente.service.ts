import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Cliente } from '../models/cliente.model';
import { ApiResponse } from '../interfaces/api-response.interface';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  buscarClientePorIdentificacion(identificacion: string): Observable<Cliente> {
  return this.http.get<any>(`${this.apiUrl}/clientes/${identificacion}`)
    .pipe(
      map(response => {
        // Si la respuesta tiene la estructura ApiResponse
        if (response.success !== undefined) {
          if (!response.success) {
            throw new Error(response.message || 'Cliente no encontrado');
          }
          return response.data;
        }
        // Si la respuesta es el objeto Cliente directamente
        return response;
      }),
      catchError(this.handleError)
    );
}

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Error desconocido';
    
    if (error.error instanceof ErrorEvent) {
      // Error del lado del cliente
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Error del lado del servidor
      if (error.status === 404) {
        errorMessage = 'Cliente no encontrado';
      } else if (error.status === 400) {
        errorMessage = 'Identificación inválida';
      } else if (error.status >= 500) {
        errorMessage = 'Error del servidor. Intente nuevamente más tarde.';
      } else {
        errorMessage = error.error?.message || error.message;
      }
    }
    
    return throwError(() => new Error(errorMessage));
  }
}