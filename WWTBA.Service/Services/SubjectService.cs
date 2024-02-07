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

        public async Task<CustomResponseDto<List<SubjectWithLessonDto>>> GetSubjectsWithDto()
        {
            List<Subject> subjects = await _subjectRepository.GetSubjectsWithLessonAsync();
            List<SubjectWithLessonDto> subjectsDto = _mapper.Map<List<SubjectWithLessonDto>>(subjects);
            return CustomResponseDto<List<SubjectWithLessonDto>>.Success(StatusCodes.Status200OK, subjectsDto);
        }

        public async Task<CustomResponseDto<SubjectWithQuestionsDto>> GetSubjectWithQuestionsDto(int subjectId)
        {
            Subject subject = await _subjectRepository.GetSubjectWithQuestionsAsync(subjectId);
            SubjectWithQuestionsDto subjectDto = _mapper.Map<SubjectWithQuestionsDto>(subject);
            return CustomResponseDto<SubjectWithQuestionsDto>.Success(StatusCodes.Status200OK, subjectDto);
        }

        public async Task<CustomResponseDto<int>> GetQuestionCountOfASubject(int subjectId)
        {
            Subject subject = await _subjectRepository.GetSubjectWithQuestionsAsync(subjectId);
            return CustomResponseDto<int>.Success(StatusCodes.Status200OK, subject.Questions.Count);
        }
    }
}