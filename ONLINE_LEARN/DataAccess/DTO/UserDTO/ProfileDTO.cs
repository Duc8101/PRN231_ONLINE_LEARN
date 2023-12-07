namespace DataAccess.DTO.UserDTO
{
    public class ProfileDTO
    {
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}
