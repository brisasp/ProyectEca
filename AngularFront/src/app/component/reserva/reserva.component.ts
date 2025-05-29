import { Component, Input,OnInit } from '@angular/core';
import { propuestaModel } from '../../models/propuestaModel';
import { CommonModule } from '@angular/common';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { NgClass } from '@angular/common';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-reserva',
  imports: [CommonModule,NgClass, DatePipe],
  standalone: true,
  templateUrl: './reserva.component.html',
  styleUrls: ['./reserva.component.css'],
})
export class ReservaComponent implements OnInit  {
  //@Input() reserva!: any;
  @Input() reserva: any;
  ngOnInit(): void {
    console.log('Reserva recibida:', this.reserva);
    }
    getEstadoTexto(estado: string): string {
    return estado; // porque ya es un string como "Pendiente", "Aprobada", "Rechazada"
  }

  }



