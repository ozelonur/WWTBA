using Microsoft.EntityFrameworkCore;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class UserTestRepository : GenericRepository<UserTest>, IUserTestRepository
    {
        public UserTestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserTestResultDto> GetTestResultsAsync(int testId)
        {
            List<UserAnswer> userAnswers = await context.UserAnswers
                .Where(x => x.UserTestId == testId)
                .ToListAsync();

            int correctAnswerCount = userAnswers.Count(x => x.IsCorrect);
            int wrongAnswerCount = userAnswers.Count(x => !x.IsCorrect);

            float totalTime = userAnswers.Sum(x => x.QuestionSolveTime);
            

            UserTestResultDto dto = new()
            {
                CorrectAnswerCount = correctAnswerCount,
                WrongAnswerCount = wrongAnswerCount,
                TestSolveTime = totalTime
            };

            return dto;
        }

    }
}

