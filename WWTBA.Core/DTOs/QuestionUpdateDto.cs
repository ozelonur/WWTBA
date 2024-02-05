namespace WWTBA.Core.DTOs
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Explanation { get; set; }
        public int SubjectId { get; set; }
    }
}

