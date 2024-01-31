using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class SubjectService : Service<Subject, SubjectDto>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMapper mapper,
            ISubjectRepository subjectRepository) : base(
            repository, unitOfWork, mapper)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<CustomResponseDto<SubjectDto>> AddAsync(SubjectCreateDto dto)
        {
            Subject newEntity = _mapper.Map<Subject>(dto);
            await _subjectRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            SubjectDto newDto = _mapper.Map<SubjectDto>(newEntity);
            return CustomResponseDto<SubjectDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<SubjectDto>> UpdateAsync(SubjectUpdateDto dto)
        {
            Subject newEntity = _mapper.Map<Subject>(dto);
            _subjectRepository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            SubjectDto newDto = _mapper.Map<SubjectDto>(newEntity);
            return CustomResponseDto<SubjectDto>.Success(StatusCodes.Status200OK, newDto);
        }
    }
}