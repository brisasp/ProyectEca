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
    using DesignAPI.Models.DTOs.ProfesorDTO;

    [Route("api/[controller]")]
    [ApiController]
    public class DiaNoLectivoController : BaseController<DiaNoLectivoEntity, DiaNoLectivoDTO, CreateDiaNoLectivoDTO>
    {
        public DiaNoLectivoController(IDiaNoLectivoRepository DiaNoLectivoRepository, IMapper mapper, ILogger<DiaNoLectivoController> logger)
            : base(DiaNoLectivoRepository, mapper, logger)
        {
        }
    }
}
