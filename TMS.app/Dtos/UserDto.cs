using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.app.Dtos
{
    public record AuthResponseDto(string Token);

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
