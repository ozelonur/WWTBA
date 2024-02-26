namespace WWTBA.Core.Models
{
    public class UserAnswer : BaseEntity
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public User User { get; set; }
    }
}

