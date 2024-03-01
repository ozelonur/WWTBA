namespace WWTBA.Core.DTOs
{
    public class UserAnswerUpdateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public int UserTestId { get; set; }
        public float QuestionSolveTime { get; set; }
    
    }
}

