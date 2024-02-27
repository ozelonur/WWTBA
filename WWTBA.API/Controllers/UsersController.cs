using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetQuestionWithAnswers(int userId)
        {
            return CreateActionResult(await _userService.GetUserWithAnswersAsync(userId));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _userService.GetAllAsync());
        }
        
        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _userService.GetByIdAsync(id));
        }
        
        [ServiceFilter(typeof(NotFoundFilter<User, UserWithAnswersDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _userService.RemoveAsync(id));
        }
        
        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _userService.AnyAsync(x => x.Id == id));
        }

        [HttpGet("CheckUID/{id}")]
        public async Task<IActionResult> CheckUID(string id)
        {
            return CreateActionResult(await _userService.AnyAsync(x => x.UniqueIdentifier == id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateDto dto)
        {
            return CreateActionResult(await _userService.AddAsync(dto));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            return CreateActionResult(await _userService.UpdateAsync(dto));
        }
    }
}

