using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class UserTestsController : CustomBaseController
    {
        private readonly IUserTestService _userTestService;

        public UserTestsController(IUserTestService userTestService)
        {
            _userTestService = userTestService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(UserTestCreateDto dto)
        {
            return CreateActionResult(await _userTestService.AddAsync(dto));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UserTestUpdateDto dto)
        {
            return CreateActionResult(await _userTestService.UpdateAsync(dto));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _userTestService.GetAllAsync());
        }

        [HttpGet("[action]/{testId}")]
        public async Task<IActionResult> GetTestResultsAsync(int testId)
        {
            return CreateActionResult(await _userTestService.GetTestResultsAsync(testId));
        }
        
        [ServiceFilter(typeof(NotFoundFilter<UserTest, UserTestDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _userTestService.GetByIdAsync(id));
        }
        
        [ServiceFilter(typeof(NotFoundFilter<UserTest, UserTestDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _userTestService.RemoveAsync(id));
        }
        
        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _userTestService.AnyAsync(x => x.Id == id));
        }
    }
}

