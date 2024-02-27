namespace WWTBA.Core.DTOs
{
    public class UserWithAnswersDto : UserDto
    {
        public List<UserAnswerDto> UserAnswers { get; set; }
    }
}

