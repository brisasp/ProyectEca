import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ReservaComponent} from 'src/app/component/reserva/reserva.component';
import { ReservaService } from 'src/app/service/reserva.service';
@Component(
{
  selector: 'app-principal',
  imports: [CommonModule, ReservaComponent, RouterLink],
  standalone: true,
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})

export class PrincipalComponent implements OnInit{

  reservasList: any[] = [];

  constructor(private reservaService: ReservaService){}
    async ngOnInit() {
    try {
      this.reservasList = await this.reservaService.getPendientes();
    } catch (error) {
      console.error('Error al cargar reservas pendientes', error);
    }
  }
}
