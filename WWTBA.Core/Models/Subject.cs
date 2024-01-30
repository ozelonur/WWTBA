namespace WWTBA.Core.Models
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}

