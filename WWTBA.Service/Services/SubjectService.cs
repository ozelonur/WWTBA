using AutoMapper;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class SubjectService : Service<Subject, SubjectDto>, ISubjectService
    {
        public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(
            repository, unitOfWork, mapper)
        {
        }
    }
}