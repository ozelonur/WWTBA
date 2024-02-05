namespace WWTBA.Core.DTOs
{
    public class AnswerDto : BaseDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}