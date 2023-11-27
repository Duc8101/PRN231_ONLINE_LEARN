using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.UserDTO
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Image { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
