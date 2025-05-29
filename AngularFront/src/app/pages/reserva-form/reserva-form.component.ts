import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-reserva-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reserva-form.component.html',
  styleUrls: ['./formulario-reserva.component.css']

})
export class ReservaFormComponent {
  @Output() reservaCreada = new EventEmitter<any>();

  reserva = {
    fecha: '',
    horaInicio: '',
    horaFin: '',
    grupo: '',
    nombreProfesor: '',
    estado: 0 // Pendiente por defecto
  };

  enviarFormulario() {
    this.reservaCreada.emit(this.reserva);
  }
}
