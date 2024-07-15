namespace Common.DTO.UserDTO
{
    public class UserCreateDTO
    {
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Username { get; set; } = null!;

    }
}
