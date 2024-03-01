namespace WWTBA.Core.Models
{
    public class UserTest : BaseEntity
    {
        public int UserId { get; set; }
        public float TestSolveTime { get; set; }
        public User User { get; set; }
    }
}

