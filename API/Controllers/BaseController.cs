﻿namespace DesignAPI.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::AutoMapper;
    using global::DesignAPI.Repository.IRepository;
    using System.Runtime.ConstrainedExecution;
    using DesignAPI.Models.Entity;

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TDto, TCreateDto> : ControllerBase
            where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        protected BaseController(IRepository<TEntity> repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}", Name = "[controller]_GetEntity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null) return NotFound();

                return Ok(_mapper.Map<TDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = _mapper.Map<List<TDto>>(await _repository.GetAllAsync());
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Create([FromBody] TCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var entity = _mapper.Map<TEntity>(createDto);
                await _repository.CreateAsync(entity);
                var dto = _mapper.Map<TDto>(entity);
                return CreatedAtRoute($"{ControllerContext.ActionDescriptor.ControllerName}_GetEntity", new { id = entity.GetHashCode() }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] TDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var entity = await _repository.GetAsync(id);
                if (entity == null) return NotFound();

                _mapper.Map(dto, entity);
                await _repository.UpdateAsync(entity);
                if (entity is ReservaEntity reserva && reserva.Estado == EstadoReserva.Rechazada)
                {
                    var emailService = HttpContext.RequestServices.GetRequiredService<EmailService>();
                    await emailService.EnviarCorreo(
                        reserva.CorreoProfesor,
                        "Reserva rechazada",
                        $@"
                <h3>Tu reserva ha sido rechazada</h3>
                <p><strong>Fecha:</strong> {reserva.Fecha:yyyy-MM-dd}</p>
                <p><strong>Hora:</strong> {reserva.HoraInicio} - {reserva.HoraFin}</p>
                <p><strong>Grupo:</strong> {reserva.Grupo}</p>
                <p><strong>Motivo:</strong> Contacta con el administrador para más información.</p>"
                    );
                }
                return Ok(_mapper.Map<TDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }


}
