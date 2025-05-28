using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Helpers
{
    public static class Constants
    {
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";


        public const string BASE_URL = "https://localhost:7016/api/";
       // public const string ALUMNO_PATH = "Alumno/";
     //   public const string PROFESOR_PATH = "Profesor/";
      //  public const string DICATADOR_URL = "Dicatador/";


        //FRANJAHORARIO
        public const string FRANJA_GET_ALL = "FranjaHorario";
        public const string FRANJA_GET_BY_ID = "FranjaHorario/{0}";
        public const string FRANJA_PATCH = "FranjaHorario/{0}";
        public const string FRANJA_POST = "FranjaHorario";


        //RESERVA
        public const string RESERVA_GET_ALL = "Reserva";
        public const string RESERVA_GET_BY_ID = "Reserva/{0}";
        public const string RESERVA_CREAR = "Reserva/crear";
        public const string RESERVA_PATCH = "Reserva/{0}";
        public const string RESERVA_CAMBIAR_ESTADO = "Reserva/estado";
        public const string RESERVA_PENDIENTES = "api/reservas/pendientes";
        public const string RESERVA_MIS_RESERVAS = "Reserva/mis-reservas";

        //DIANOLECTIVO
        public const string DIA_NO_LECTIVO_GET_ALL = "DiaNoLectivo";
        public const string DIA_NO_LECTIVO_GET_BY_ID = "DiaNoLectivo/{0}";
        public const string DIA_NO_LECTIVO_POST = "DiaNoLectivo";
        public const string DIA_NO_LECTIVO_PATCH = "DiaNoLectivo/{0}";


        public const string LOGIN_PATH = "users/login";
        public const string REGISTER_PATH = "users/register";

    }
}
