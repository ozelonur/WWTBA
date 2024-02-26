namespace WWTBA.Core.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string FirebaseToken { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}

