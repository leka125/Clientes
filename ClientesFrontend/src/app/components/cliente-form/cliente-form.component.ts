import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-cliente-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.scss']
})
export class ClienteFormComponent {
  @Output() buscarCliente = new EventEmitter<string>();
  
  clienteForm: FormGroup;
  
  constructor(private fb: FormBuilder) {
    this.clienteForm = this.fb.group({
      identificacion: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.pattern('^[a-zA-Z0-9]*$')
      ]]
    });
  }

  onSubmit(): void {
    if (this.clienteForm.valid) {
      this.buscarCliente.emit(this.clienteForm.get('identificacion')?.value);
    }
  }

  get identificacion() {
    return this.clienteForm.get('identificacion');
  }
}