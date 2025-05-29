import { Component, EventEmitter,Input ,Output } from '@angular/core';
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
  @Input() fechaSeleccionada: string = '';
  @Output() reservaCreada = new EventEmitter<any>();

  reserva = {
    fecha: '',
    horaInicio: '',
    horaFin: '',
    grupo: '',
    nombreProfesor: '',
    correo: '', 
    estado: 0 // Pendiente por defecto
  };
ngOnInit(): void {
    this.reserva.fecha = this.fechaSeleccionada;
  }

  crearReserva() {
    // Aquí haces la llamada a la API
    console.log('Reserva creada', this.reserva);
    this.reservaCreada.emit();
  }
}
