using Microsoft.EntityFrameworkCore;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserAnswerRepository : GenericRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserAverageDto> GetTotalAverageAsync(int userId)
        {
            List<UserAnswer> userAnswers =
                await context.UserAnswers.Where(x => x.UserId == userId).ToListAsync();

            int correctAnswerCount = userAnswers.Count(x => x.IsCorrect);
            int wrongAnswerCount = userAnswers.Count(x => !x.IsCorrect);

            int totalAnswers = correctAnswerCount + wrongAnswerCount;

            float correctAnswerRatio = totalAnswers > 0 ? (float)correctAnswerCount / totalAnswers : 0;

            correctAnswerRatio *= 100f;


            float average = userAnswers.Average(x => x.QuestionSolveTime);

            UserAverageDto dto = new()
            {
                CorrectAnswers = correctAnswerCount,
                WrongAnswers = wrongAnswerCount,
                AverageQuestionSolveTime = average,
                Accuracy = correctAnswerRatio
            };

            return dto;
        }

        public async Task<UserSubjectAverageDto> GetSubjectSpecificAverageAsync(int userId, int subjectId)
        {
            List<UserAnswer> userAnswers = await context.UserAnswers
                .Where(x => x.UserId == userId &&
                            context.Questions.Any(q => q.Id == x.QuestionId && q.SubjectId == subjectId))
                .ToListAsync();

            int correctAnswerCount = userAnswers.Count(x => x.IsCorrect);
            int wrongAnswerCount = userAnswers.Count(x => !x.IsCorrect);

            int totalAnswers = correctAnswerCount + wrongAnswerCount;

            float correctAnswerRatio = totalAnswers > 0 ? (float)correctAnswerCount / totalAnswers : 0;

            correctAnswerRatio *= 100f;

            string subjectName = (await context.Subjects.Where(x => x.Id == subjectId).SingleOrDefaultAsync()).Name;

            float average = userAnswers.Average(x => x.QuestionSolveTime);

            UserSubjectAverageDto dto = new()
            {
                CorrectAnswers = correctAnswerCount,
                WrongAnswers = wrongAnswerCount,
                Accuracy = correctAnswerRatio,
                AverageQuestionSolveTime = average,
                SubjectName = subjectName
            };

            return dto;
        }

        public async Task<UserAnalysisDto> GetUserAnalysisAsync(int userId)
        {
            var subjectAnalysis = await context.UserAnswers
                .Where(ua => ua.UserId == userId)
                .Join(context.Questions, ua => ua.QuestionId, q => q.Id,
                    (ua, q) => new { ua.IsCorrect, ua.QuestionSolveTime, q.SubjectId })
                .GroupBy(x => x.SubjectId)
                .Select(g => new
                {
                    SubjectId = g.Key,
                    TotalCount = g.Count(),
                    CorrectCount = g.Count(x => x.IsCorrect),
                    WrongCount = g.Count(x => !x.IsCorrect),
                    CorrectRatio = (float)g.Count(x => x.IsCorrect) / g.Count(),
                    WrongRatio = (float)g.Count(x => !x.IsCorrect) / g.Count(),
                    AverageSolveTime = g.Average(x => x.QuestionSolveTime)
                })
                .ToListAsync();

            List<int> subjectIds = subjectAnalysis.Select(sa => sa.SubjectId).Distinct().ToList();
            Dictionary<int, string> subjects = await context.Subjects.Where(s => subjectIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id, s => s.Name);

            var mostCorrectSubject = subjectAnalysis.MaxBy(x => x.CorrectRatio);
            var mostWrongSubject = subjectAnalysis.MaxBy(x => x.WrongRatio);
            var fastestSolveTimeSubject = subjectAnalysis.MinBy(x => x.AverageSolveTime);
            var slowestSolveTimeSubject = subjectAnalysis.MaxBy(x => x.AverageSolveTime);

            UserAnalysisDto dto = new()
            {
                MostCorrectSubjectName = subjects[mostCorrectSubject.SubjectId],
                MostCorrectRatio = mostCorrectSubject.CorrectRatio,
                MostCorrectSubjectCorrectCount = mostCorrectSubject.CorrectCount,
                MostCorrectSubjectWrongCount = mostCorrectSubject.WrongCount,
                MostWrongSubjectName = subjects[mostWrongSubject.SubjectId],
                MostWrongRatio = mostWrongSubject.WrongRatio,
                MostWrongSubjectCorrectCount = mostWrongSubject.CorrectCount,
                MostWrongSubjectWrongCount = mostWrongSubject.WrongCount,
                FastestSolveTimeSubjectName = subjects[fastestSolveTimeSubject.SubjectId],
                FastestSolveTimeAverage = fastestSolveTimeSubject.AverageSolveTime,
                SlowestSolveTimeSubjectName = subjects[slowestSolveTimeSubject.SubjectId],
                SlowestSolveTimeAverage = slowestSolveTimeSubject.AverageSolveTime
            };

            return dto;
        }
    }
}