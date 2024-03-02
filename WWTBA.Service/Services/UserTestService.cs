using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class UserTestService : Service<UserTest, UserTestDto>, IUserTestService
    {
        private readonly IUserTestRepository _userTestRepository;

        public UserTestService(IGenericRepository<UserTest> repository, IUnitOfWork unitOfWork, IMapper mapper,
            IUserTestRepository userTestRepository) : base(repository, unitOfWork, mapper)
        {
            _userTestRepository = userTestRepository;
        }

        public async Task<CustomResponseDto<UserTestDto>> AddAsync(UserTestCreateDto dto)
        {
            UserTest newTest = _mapper.Map<UserTest>(dto);
            await _userTestRepository.AddAsync(newTest);
            await _unitOfWork.CommitAsync();
            UserTestDto newDto = _mapper.Map<UserTestDto>(newTest);
            return CustomResponseDto<UserTestDto>.Success(StatusCodes.Status200OK, newDto);
        }
        
        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserTestUpdateDto dto)
        {
            UserTest newTest = _mapper.Map<UserTest>(dto);
            _userTestRepository.Update(newTest);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<UserTestResultDto>> GetTestResultsAsync(int testId)
        {
            UserTestResultDto dto = await _userTestRepository.GetTestResultsAsync(testId);
            return CustomResponseDto<UserTestResultDto>.Success(StatusCodes.Status200OK, dto);
        }
    }
}