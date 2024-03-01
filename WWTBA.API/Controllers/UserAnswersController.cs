using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class UserAnswersController : CustomBaseController
    {
        private readonly IUserAnswerService _userAnswerService;

        public UserAnswersController(IUserAnswerService userAnswerService)
        {
            _userAnswerService = userAnswerService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(UserAnswerCreateDto dto)
        {
            return CreateActionResult(await _userAnswerService.AddAsync(dto));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UserAnswerUpdateDto dto)
        {
            return CreateActionResult(await _userAnswerService.UpdateAsync(dto));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _userAnswerService.GetAllAsync());
        }
        
        [ServiceFilter(typeof(NotFoundFilter<UserAnswer, UserAnswerDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _userAnswerService.GetByIdAsync(id));
        }
        
        [ServiceFilter(typeof(NotFoundFilter<UserAnswer, UserAnswerDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _userAnswerService.RemoveAsync(id));
        }
        
        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _userAnswerService.AnyAsync(x => x.Id == id));
        }
    }
}

