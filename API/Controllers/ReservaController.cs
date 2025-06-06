﻿namespace DesignAPI.Controllers
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
    using Humanizer;

    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : BaseController<ReservaEntity, UserDto, CreateReservaDTO>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IDiaNoLectivoRepository _diaNoLectivoRepository;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;

        public ReservaController(IReservaRepository ReservaRepository, IDiaNoLectivoRepository diaNoLectivoRepository, IMapper mapper, ILogger<ReservaController> logger, EmailService emailService, IConfiguration config)
            : base(ReservaRepository, mapper, logger)
        {
            _reservaRepository = ReservaRepository;
            _diaNoLectivoRepository = diaNoLectivoRepository;
            _emailService = emailService;
            _config = config;
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

        [HttpPost]
        [Authorize(Roles = "admin, profesor")]
        public override async Task<IActionResult> Create([FromBody] CreateReservaDTO dto)
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
            //var correo = "Rafael";//User?.Claims?.FirstOrDefault(c => c.Type == "email")?.Value ?? "sin-correo";
            //var nombre = "Rafael@ejemplo.com";//User?.Claims?.FirstOrDefault(c => c.Type == "name")?.Value ?? "Usuario desconocido";

            var reserva = _mapper.Map<ReservaEntity>(dto);
            //reserva.CorreoProfesor = correo;
            //reserva.NombreProfesor = nombre;
            reserva.Estado = EstadoReserva.Pendiente;
            reserva.FechaSolicitud = DateTime.UtcNow;

            await _reservaRepository.CreateAsync(reserva);
            await _reservaRepository.Save();

            var adminEmail = _config["EmailSettings:AdminEmail"];
            await _emailService.EnviarCorreo(
                 adminEmail,
                 "Nueva solicitud de reserva",
                 $@"
                <h3>Se ha realizado una nueva reserva</h3>
                <p><strong>Profesor:</strong> {dto.NombreProfesor} ({dto.CorreoProfesor})</p>
                <p><strong>Fecha:</strong> {dto.Fecha:yyyy-MM-dd}</p>
                <p><strong>Hora:</strong> {dto.HoraInicio} - {dto.HoraFin}</p>
                <p><strong>Grupo:</strong> {dto.Grupo}</p>
                <p><strong>Estado:</strong> Pendiente</p>"
             );

            return Ok(new { mensaje = "Reserva solicitada correctamente." });
        }
    }
}
