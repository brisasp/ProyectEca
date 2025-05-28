
using AutoMapper;
using DesignAPI.Models.DTOs;
using DesignAPI.Models.Entity;
using DesignAPI.Models.DTOs.ProfesorDTO;
using DesignAPI.Models.DTOs.ReservaDTO;
using DesignAPI.Models.DTOs.FranjaHorarioDTO;
using RestAPI.Models.Entity;


namespace DesignAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            CreateMap<ReservaEntity, UserDto>().ReverseMap();
            CreateMap<ReservaEntity, CreateReservaDTO>().ReverseMap();

            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<DiaNoLectivoEntity, DiaNoLectivoDTO>().ReverseMap();
            CreateMap<DiaNoLectivoEntity, CreateDiaNoLectivoDTO>().ReverseMap();

            CreateMap<FranjaHorarioEntity, FranjaHorarioDTO>().ReverseMap();
            CreateMap<FranjaHorarioEntity, CreateFranjaHorarioDTO>().ReverseMap();
            CreateMap<FranjaHorarioEntity, ActualizarFranjaHorarioDTO>().ReverseMap();


        }
    }
}
