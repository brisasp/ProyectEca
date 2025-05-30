export interface CreateReserva {
  fecha: string;           // formato "2025-08-26"
  horaInicio: string;      // "11:00"
  horaFin: string;         // "12:00"
  grupo: string;
  nombreProfesor: string;
  estado: string;          // "Pendiente", "Aprobada", "Rechazada"
}
