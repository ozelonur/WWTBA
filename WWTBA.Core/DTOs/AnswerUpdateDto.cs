namespace WWTBA.Core.DTOs;

public class AnswerUpdateDto
{
    public int Id { get; set; }
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}