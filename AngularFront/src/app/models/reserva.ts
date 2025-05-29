export interface Reserva {
  id: number;
  fecha: string;           // formato tipo "2025-08-26"
  horaInicio: string;      // ejemplo: "11:00"
  horaFin: string;         // ejemplo: "12:00"
  grupo: string;
  nombreProfesor: string;
  estado: 'Pendiente' | 'Aprobada' | 'Rechazada';          // 0 = Pendiente, 1 = Aprobada, 2 = Rechazada
}
