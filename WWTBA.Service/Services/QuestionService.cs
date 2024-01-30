using AutoMapper;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class QuestionService : Service<Question, QuestionDto>, IQuestionService
    {
        public QuestionService(IGenericRepository<Question> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(
            repository, unitOfWork, mapper)
        {
        }
    }
}