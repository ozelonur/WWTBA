using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : base(repository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomResponseDto<UserDto>> AddAsync(UserCreateDto dto)
        {
            User newUser = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            UserDto newDto = _mapper.Map<UserDto>(newUser);
            return CustomResponseDto<UserDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<UserWithAnswersDto>> GetUserWithAnswersAsync(int userId)
        {
            User user = await _userRepository.GetUserWithAnswersAsync(userId);
            UserWithAnswersDto userDto = _mapper.Map<UserWithAnswersDto>(user);
            return CustomResponseDto<UserWithAnswersDto>.Success(StatusCodes.Status200OK, userDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(UserUpdateDto dto)
        {
            User newUser = _mapper.Map<User>(dto);
            _userRepository.Update(newUser);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
    }
}

