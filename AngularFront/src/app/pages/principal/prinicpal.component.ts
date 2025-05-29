import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ReservaFormComponent } from '../../pages/reserva-form/reserva-form.component';
import { ReservaComponent} from 'src/app/component/reserva/reserva.component';
import { ReservaService } from 'src/app/service/reserva.service';
@Component(
{
  selector: 'app-principal',
  imports: [CommonModule, ReservaComponent, RouterLink, FormsModule,ReservaFormComponent],
  standalone: true,
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})

export class PrincipalComponent implements OnInit{

  //reservasList: any[] = [];

 // constructor(private reservaService: ReservaService){}
    //async ngOnInit() {
   // try {
   //   this.reservasList = await this.reservaService.getPendientes();
    //} catch (error) {
   //   console.error('Error al cargar reservas pendientes', error);
    //}
  //}
  mostrarFormulario = false;
 reservasAgrupadas: { [fecha: string]: any[] } = {};
 fechaFiltro: string = '';
 reservasTotales: any[] = [];

  constructor(private reservaService: ReservaService, private router: Router) {}

  async ngOnInit() {
    try {
      //const reservas = await this.reservaService.getPendientes();
      //this.reservasAgrupadas = this.agruparPorFecha(reservas);
      this.reservasTotales = await this.reservaService.getPendientes();
    this.reservasAgrupadas = this.agruparPorFecha(this.reservasTotales);
    } catch (error) {
      console.error('Error al cargar reservas pendientes', error);
    }
  }
  filtrarPorFecha() {
  if (this.fechaFiltro) {
    const filtradas = this.reservasTotales.filter(reserva =>
      reserva.fecha.startsWith(this.fechaFiltro)
    );
    this.reservasAgrupadas = this.agruparPorFecha(filtradas);
  } else {
    this.reservasAgrupadas = this.agruparPorFecha(this.reservasTotales);
  }
}
  agruparPorFecha(reservas: any[]): { [fecha: string]: any[] } {
    const agrupadas: { [fecha: string]: any[] } = {};
    reservas.forEach((reserva) => {
      const fecha = reserva.fecha.split('T')[0]; // solo yyyy-MM-dd
      if (!agrupadas[fecha]) {
        agrupadas[fecha] = [];
      }
      agrupadas[fecha].push(reserva);
    });
    return agrupadas;
  }

  limpiarFiltro() {
  this.fechaFiltro = '';
  this.reservasAgrupadas = this.agruparPorFecha(this.reservasTotales);
}

  irAReserva() {
  this.router.navigate(['/nueva-reserva']); // Ajusta la ruta si la llamas distinto
}
toggleFormulario() {
    this.mostrarFormulario = !this.mostrarFormulario;
  }

cerrarFormulario() {
  this.mostrarFormulario = false;
}

guardarReserva(reserva: any) {
  console.log('Reserva guardada:', reserva);
  // Aquí puedes llamar a tu servicio para hacer POST a la API
  this.mostrarFormulario = false;
}

}

