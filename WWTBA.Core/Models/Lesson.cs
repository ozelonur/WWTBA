namespace WWTBA.Core.Models
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}

