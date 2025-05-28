

using RestAPI.Models.Entity;

namespace DesignAPI.Models.DTOs.UserDTO
{
    public class UserLoginResponseDto
    {
        public AppUser User { get; set; }
        public string Token { get; set; }

    }
}
