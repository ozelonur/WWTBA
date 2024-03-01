namespace WWTBA.Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string FirebaseToken { get; set; }
        public string UniqueIdentifier { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}

