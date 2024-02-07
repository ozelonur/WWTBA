using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class AnswerService : Service<Answer, AnswerDto>, IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IGenericRepository<Answer> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IAnswerRepository answerRepository) : base(
            repository, unitOfWork, mapper)
        {
            _answerRepository = answerRepository;
        }

        public async Task<CustomResponseDto<AnswerDto>> AddAsync(AnswerCreateDto dto)
        {
            Answer newEntity = _mapper.Map<Answer>(dto);
            await _answerRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            AnswerDto newDto = _mapper.Map<AnswerDto>(newEntity);
            return CustomResponseDto<AnswerDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(AnswerUpdateDto dto)
        {
            Answer newEntity = _mapper.Map<Answer>(dto);
            _answerRepository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<List<AnswerDto>>> AddRangeAsync(List<AnswerCreateDto> dto)
        {
            List<Answer> newEntities = _mapper.Map<List<Answer>>(dto);
            await _answerRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            List<AnswerDto> newDtos = _mapper.Map<List<AnswerDto>>(newEntities);
            return CustomResponseDto<List<AnswerDto>>.Success(StatusCodes.Status200OK, newDtos);
        }
    }
}