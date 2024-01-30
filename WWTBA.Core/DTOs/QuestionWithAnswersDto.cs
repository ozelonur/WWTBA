namespace WWTBA.Core.DTOs
{
    public class QuestionWithAnswersDto : QuestionDto
    {
        public List<AnswerDto> Answers { get; set; }
    }
}

