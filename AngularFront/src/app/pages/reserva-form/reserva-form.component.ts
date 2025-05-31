import { Component, EventEmitter,Input ,Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FranjaHorario } from '../../models/franja-horaria.model';

import { CreateReserva } from 'src/app/models/create-reserva.model';
import { ReservaService } from 'src/app/service/reserva.service'; 

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
  franjas: FranjaHorario[] = [];
  diasNoLectivos: string[] = [];



 reserva: CreateReserva = {
  fecha: '',
  horaInicio: '',
  horaFin: '',
  grupo: '',
  nombreProfesor: '',
  correoProfesor: '',
  estado: 'Pendiente'
};

  constructor(private reservaService: ReservaService) {}
async ngOnInit():Promise<void>  {
  if (this.fechaSeleccionada) {
     const fecha = new Date(this.fechaSeleccionada);
      const year = fecha.getFullYear();
      const month = String(fecha.getMonth() + 1).padStart(2, '0');
      const day = String(fecha.getDate()).padStart(2, '0');
      this.reserva.fecha = `${year}-${month}-${day}`;
    } else {
      console.warn('No se recibió fechaSeleccionada como @Input.');
    }

 this.franjas = (await this.reservaService.getFranjas()).filter(f => f.activa);
 await this.cargarDiasNoLectivos();
  }

  actualizarHoraFin() {
    const seleccionada = this.franjas.find(f => f.horaInicio === this.reserva.horaInicio);
    if (seleccionada) {
      this.reserva.horaFin = seleccionada.horaFin;
    }
  }

  async cargarDiasNoLectivos() {
  try {
    const res = await this.reservaService.getDiasNoLectivos();
    this.diasNoLectivos = res.map((d: any) => d.fecha.split('T')[0]); // ajusta si tu DTO se llama distinto
  } catch (error) {
    console.error('Error cargando días no lectivos:', error);
  }
}
 validarFecha(fechaSeleccionada: string) {
    if (this.diasNoLectivos.includes(fechaSeleccionada)) {
      alert('No puedes reservar en un día no lectivo');
      this.reserva.fecha = '';
    }
  }

async crearReserva() {
  try {
    // Mostrar por consola los datos exactos que se envían
    console.log('Reserva a enviar:', JSON.stringify(this.reserva, null, 2));

    const respuesta = await this.reservaService.crearReserva(this.reserva);

    console.log('Reserva guardada con éxito:', respuesta);
    this.reservaCreada.emit(); // Notifica al padre
  } catch (error) {
    console.error('Error al guardar la reserva:', error);
    alert('Error al guardar la reserva');
  }
}

}