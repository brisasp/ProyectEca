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
    using DesignAPI.Models.DTOs.FranjaHorarioDTO;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class FranjaHorarioController : BaseController<FranjaHorarioEntity, FranjaHorarioDTO, CreateFranjaHorarioDTO>
    {
        private readonly IFranjaHorarioRepository _franjaHorarioRepository;
        private readonly IReservaRepository _reservaRepository;
        public FranjaHorarioController(IFranjaHorarioRepository FranjaHorarioRepository,IReservaRepository reservaRepository, IMapper mapper, ILogger<FranjaHorarioController> logger)
            : base(FranjaHorarioRepository, mapper, logger)
        {
            _franjaHorarioRepository = FranjaHorarioRepository;
            _reservaRepository = reservaRepository;
        }
        // GET: api/franjahorario/activos
        [Authorize(Roles = "admin,profesor")]
        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos()
        {
            var franjas = await _franjaHorarioRepository.GetActivosAsync();
            var resultado = _mapper.Map<IEnumerable<FranjaHorarioDTO>>(franjas);
            return Ok(resultado);
        }


        // GET: api/franjahorario/disponibles?fecha=2025-05-08
        [Authorize(Roles = "admin,profesor")]
        [HttpGet("disponibles")]

        public async Task<IActionResult> GetDisponibles([FromQuery] DateTime fecha)
        {
            var reservas = await _reservaRepository.GetByFechaAsync(fecha);

            var franjas = await _franjaHorarioRepository.GetDisponiblesParaFechaAsync(fecha, reservas.ToList());
            var resultado = _mapper.Map<IEnumerable<FranjaHorarioDTO>>(franjas);

            return Ok(resultado);
        }
    }
}
 