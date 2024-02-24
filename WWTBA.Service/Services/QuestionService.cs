using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class QuestionService : Service<Question, QuestionDto>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IGenericRepository<Question> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IQuestionRepository questionRepository) : base(
            repository, unitOfWork, mapper)
        {
            _questionRepository = questionRepository;
        }

        public async Task<CustomResponseDto<QuestionDto>> AddAsync(QuestionCreateDto dto)
        {
            Question newEntity = _mapper.Map<Question>(dto);
            await _questionRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            QuestionDto newDto = _mapper.Map<QuestionDto>(newEntity);
            return CustomResponseDto<QuestionDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<QuestionDto>>> AddRangeAsync(IEnumerable<QuestionCreateDto> dtos)
        {
            IEnumerable<Question> newEntities = _mapper.Map<IEnumerable<Question>>(dtos);
            await _questionRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            IEnumerable<QuestionDto> newDtos = _mapper.Map<IEnumerable<QuestionDto>>(newEntities);
            return CustomResponseDto<IEnumerable<QuestionDto>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(QuestionUpdateDto dto)
        {
            Question newEntity = _mapper.Map<Question>(dto);
            _questionRepository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<List<QuestionWithSubjectDto>>> GetQuestionsWithSubject()
        {
            List<Question> questions = await _questionRepository.GetQuestionsWithSubjectAsync();
            List<QuestionWithSubjectDto> questionsDto = _mapper.Map<List<QuestionWithSubjectDto>>(questions);
            return CustomResponseDto<List<QuestionWithSubjectDto>>.Success(StatusCodes.Status200OK, questionsDto);
        }

        public async Task<CustomResponseDto<QuestionWithAnswersDto>> GetQuestionWithAnswersAsync(int questionId)
        {
            Question question = await _questionRepository.GetQuestionWithAnswersAsync(questionId);
            QuestionWithAnswersDto questionDto = _mapper.Map<QuestionWithAnswersDto>(question);
            return CustomResponseDto<QuestionWithAnswersDto>.Success(StatusCodes.Status200OK, questionDto);
        }
    }
}