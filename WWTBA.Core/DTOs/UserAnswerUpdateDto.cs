namespace WWTBA.Core.DTOs
{
    public class UserAnswerUpdateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    
    }
}

