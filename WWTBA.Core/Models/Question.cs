namespace WWTBA.Core.Models
{
    public class Question : BaseEntity
    {
        public string QuestionText { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}

