namespace Common.DTO.CourseDTO
{
    public class CourseCreateUpdateDTO
    {
        public string CourseName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
    }
}
