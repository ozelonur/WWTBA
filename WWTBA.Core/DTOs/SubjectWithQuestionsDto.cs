namespace WWTBA.Core.DTOs
{
    public class SubjectWithQuestionsDto : SubjectDto
    {
        public List<QuestionDto> Questions { get; set; }
    }
}

