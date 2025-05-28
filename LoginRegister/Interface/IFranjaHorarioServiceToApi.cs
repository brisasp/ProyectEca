using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Interface
{
    public interface IFranjaHorarioServiceToApi
    {
        Task<IEnumerable<FranjaHorarioDTO>> GetFranjas();
        Task<FranjaHorarioDTO> GetFranjaById(int id);
        Task PostFranja(FranjaHorarioDTO franja);
        Task PutFranja(FranjaHorarioDTO franja);
        Task DeleteFranja(int id);
    }
}
