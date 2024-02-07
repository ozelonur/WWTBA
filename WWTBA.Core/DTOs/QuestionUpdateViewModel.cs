namespace WWTBA.Core.DTOs
{
    public class QuestionUpdateViewModel
    {
        public QuestionUpdateDto QuestionUpdateDto { get; set; }
        public List<AnswerUpdateDto> AnswerUpdateDtos { get; set; } = new();
    }
}

