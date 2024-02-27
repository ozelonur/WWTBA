namespace WWTBA.Core.DTOs
{
    public class UserAnswerCreateDto
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}

