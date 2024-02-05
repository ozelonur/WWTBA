using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers;

public class AnswerController : CustomBaseController
{
    private readonly IAnswerService _answerService;

    public AnswerController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(AnswerCreateDto dto)
    {
        return CreateActionResult(await _answerService.AddAsync(dto));
    }

    [ServiceFilter(typeof(Web.Filters.NotFoundFilter<Answer, AnswerDto>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return CreateActionResult(await _answerService.GetByIdAsync(id));
    }

    [ServiceFilter(typeof(NotFoundFilter<Answer, AnswerUpdateDto>))]
    [HttpPut]
    public async Task<IActionResult> Update(AnswerUpdateDto dto)
    {
        return CreateActionResult(await _answerService.UpdateAsync(dto));
    }

    [ServiceFilter(typeof(NotFoundFilter<Answer, AnswerDto>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        return CreateActionResult(await _answerService.RemoveAsync(id));
    }

    [HttpGet("Any/{id}")]
    public async Task<IActionResult> Any(int id)
    {
        return CreateActionResult(await _answerService.AnyAsync(x => x.Id == id));
    }

    [HttpPost("SaveAll")]
    public async Task<IActionResult> SaveAll(List<AnswerCreateDto> dtos)
    {
        return CreateActionResult(await _answerService.AddRangeAsync(dtos));
    }

    [HttpDelete("DeleteAll")]
    public async Task<IActionResult> RemoveAll(List<int> ids)
    {
        return CreateActionResult(await _answerService.RemoveRangeAsync(ids));
    }
}