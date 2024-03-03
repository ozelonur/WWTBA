namespace WWTBA.Core.DTOs
{
    public class UserAnalysisDto
    {
        public string MostCorrectSubjectName { get; set; }
        public float MostCorrectRatio { get; set; }
        public int MostCorrectSubjectCorrectCount { get; set; }
        public int MostCorrectSubjectWrongCount { get; set; }
        public string MostWrongSubjectName { get; set; }
        public float MostWrongRatio { get; set; }
        public int MostWrongSubjectCorrectCount { get; set; }
        public int MostWrongSubjectWrongCount { get; set; }
        public string FastestSolveTimeSubjectName { get; set; }
        public float FastestSolveTimeAverage { get; set; }
        public string SlowestSolveTimeSubjectName { get; set; }
        public float SlowestSolveTimeAverage { get; set; }
        
    }
}

