using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class UserAnswerService : Service<UserAnswer, UserAnswerDto>, IUserAnswerService
    {
        private readonly IUserAnswerRepository _userAnswerRepository;

        public UserAnswerService(IGenericRepository<UserAnswer> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IUserAnswerRepository userAnswerRepository) : base(repository, unitOfWork, mapper)
        {
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task<CustomResponseDto<UserAnswerDto>> AddAsync(UserAnswerCreateDto dto)
        {
            UserAnswer newAnswer = _mapper.Map<UserAnswer>(dto);
            await _userAnswerRepository.AddAsync(newAnswer);
            await _unitOfWork.CommitAsync();
            UserAnswerDto newDto = _mapper.Map<UserAnswerDto>(newAnswer);
            return CustomResponseDto<UserAnswerDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserAnswerUpdateDto dto)
        {
            UserAnswer newAnswer = _mapper.Map<UserAnswer>(dto);
            _userAnswerRepository.Update(newAnswer);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
    }
}