namespace WWTBA.Core.DTOs
{
    public class QuestionDto : BaseDto
    {
        public string QuestionText { get; set; }
        public string Explanation { get; set; }
        public int SubjectId { get; set; }
    }
}

