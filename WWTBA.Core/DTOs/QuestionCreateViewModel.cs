namespace WWTBA.Core.DTOs
{
    public class QuestionCreateViewModel
    {
        public QuestionCreateDto QuestionCreateDto { get; set; }
        public List<AnswerCreateDto> AnswerCreateDtos { get; set; } = new();
    }
}

