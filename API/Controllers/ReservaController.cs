namespace DesignAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using DesignAPI.Models.DTOs;
    using DesignAPI.Models.Entity;
    using DesignAPI.Repository;

    using DesignAPI.Repository.IRepository;
    using DesignAPI.Controllers;
    using global::AutoMapper;
    using DesignAPI.Models.DTOs.ReservaDTO;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : BaseController<ReservaEntity, UserDto, CreateReservaDTO>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IDiaNoLectivoRepository _diaNoLectivoRepository;
        public ReservaController(IReservaRepository ReservaRepository, IDiaNoLectivoRepository diaNoLectivoRepository, IMapper mapper, ILogger<ReservaController> logger)
            : base(ReservaRepository, mapper, logger)
        {
            _reservaRepository = ReservaRepository;
            _diaNoLectivoRepository = diaNoLectivoRepository;
        }

        // GET: api/reserva/pendientes
       [HttpGet("pendientes")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPendientes()
        {
            var pendientes = await _reservaRepository.GetPendientesAsync();
            var resultado = _mapper.Map<IEnumerable<UserDto>>(pendientes);
            return Ok(resultado);
        }

        // GET: api/reserva/mis-reservas [Authorize] // cualquier usuario autenticado (profesor o admin)
        [HttpGet("mis-reservas")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMisReservas()
        {
            // Tomamos el correo del claim (de Google o del login)
            var correo = User?.Claims?.FirstOrDefault(c => c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(correo))
                return Unauthorized("No se pudo obtener el correo del usuario autenticado.");

            var reservas = await _reservaRepository.GetByCorreoProfesorAsync(correo);
            var resultado = _mapper.Map<IEnumerable<UserDto>>(reservas);

            return Ok(resultado);
        }

        // POST: api/reserva [Authorize] // profesores y admins
        [HttpPost("crear")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CrearReserva([FromBody] CreateReservaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar si la fecha es un día no lectivo
            var esNoLectivo = (await _diaNoLectivoRepository.GetAllAsync())
                .Any(d => d.Fecha.Date == dto.Fecha.Date);

            if (esNoLectivo)
                return BadRequest("No se puede reservar en un día no lectivo.");
            // Convertir horaInicio y horaFin de string a TimeSpan
            TimeSpan horaInicio;
            TimeSpan horaFin;

            try
            {
                horaInicio = TimeSpan.Parse(dto.HoraInicio);  // Convertir horaInicio
                horaFin = TimeSpan.Parse(dto.HoraFin);        // Convertir horaFin
            }
            catch (FormatException)
            {
                return BadRequest("Formato de hora inválido.");
            }

            // Validar si ya hay una reserva para esa fecha y hora
            var yaExiste = await _reservaRepository.ExisteReservaEnHorarioAsync(dto.Fecha, horaInicio);

            if (yaExiste)
                return BadRequest("Ya existe una reserva para esa franja horaria.");


            // Obtener información del usuario autenticado
            var correo = "Rafael";//User?.Claims?.FirstOrDefault(c => c.Type == "email")?.Value ?? "sin-correo";
            var nombre = "Rafael@ejemplo.com";//User?.Claims?.FirstOrDefault(c => c.Type == "name")?.Value ?? "Usuario desconocido";

            var reserva = _mapper.Map<ReservaEntity>(dto);
            reserva.CorreoProfesor = correo;
            reserva.NombreProfesor = nombre;
            reserva.Estado = EstadoReserva.Pendiente;
            reserva.FechaSolicitud = DateTime.UtcNow;

            await _reservaRepository.CreateAsync(reserva);
            await _reservaRepository.Save();

            return Ok(new { mensaje = "Reserva solicitada correctamente." });



        }

    }

}
