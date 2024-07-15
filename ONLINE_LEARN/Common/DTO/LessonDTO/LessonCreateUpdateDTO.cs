namespace Common.DTO.LessonDTO
{
    public class LessonCreateUpdateDTO
    {
        public string LessonName { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
