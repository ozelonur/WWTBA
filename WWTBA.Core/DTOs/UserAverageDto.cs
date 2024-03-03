namespace WWTBA.Core.DTOs
{
    public class UserAverageDto
    {
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public float Accuracy { get; set; }
        public float AverageQuestionSolveTime { get; set; }
    }
}

