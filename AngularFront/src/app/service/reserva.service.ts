import { Injectable } from '@angular/core';
import { propuestaModel } from '../models/propuestaModel';
import { CreatePropuestaModel } from '../models/CreatePropuestaModel';
import { CreateUsuarioModel } from '../models/create-usuario-model';
import { UsuarioModel } from '../models/usuario-model';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {
  readonly baseUrl = 'https://localhost:7016/api/Reserva';


  private getAuthHeaders(): { [key: string]: string } {
    const token = localStorage.getItem('token');
    return {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };
  }

   async getPendientes(): Promise<any[]> {
    const res = await fetch(`${this.baseUrl}`, {
      headers: this.getAuthHeaders()
    });
    return await res.json();
  }
 async getMisReservas(): Promise<any[]> {
    const res = await fetch(`${this.baseUrl}/mis-reservas`, {
      headers: this.getAuthHeaders()
    });
    return await res.json();
  }

  async crearReserva(data: any): Promise<any> {
    const res = await fetch(`${this.baseUrl}/crear`, {
      method: 'POST',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(data)
    });
    return await res.json();
  }

  async cambiarEstado(reservaId: number, estado: string): Promise<any> {
    const body = { reservaId, estado };
    const res = await fetch(`${this.baseUrl}/estado`, {
      method: 'PUT',
      headers: this.getAuthHeaders(),
      body: JSON.stringify(body)
    });
    return await res.json();
  }

 async getTodasReservas(): Promise<any[]> {
  const res = await fetch(`${this.baseUrl}`, {
    headers: this.getAuthHeaders()
  });
  return await res.json();
}


  async getReservaById(id: number): Promise<any> {
    const res = await fetch(`${this.baseUrl}/${id}`, {
      headers: this.getAuthHeaders()
    });
    return await res.json();
  }
}

