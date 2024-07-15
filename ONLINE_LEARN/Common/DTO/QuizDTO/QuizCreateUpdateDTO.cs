namespace Common.DTO.QuizDTO
{
    public class QuizCreateUpdateDTO
    {
        public string Question { get; set; } = string.Empty;
        public Guid LessonId { get; set; }
        public string Answer1 { get; set; } = string.Empty;
        public string Answer2 { get; set; } = string.Empty;
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public int AnswerCorrect { get; set; }
    }
}
