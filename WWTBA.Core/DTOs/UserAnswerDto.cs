namespace WWTBA.Core.DTOs
{
    public class UserAnswerDto : BaseDto
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}

