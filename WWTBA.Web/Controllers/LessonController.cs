using Microsoft.AspNetCore.Mvc;
using WWTBA.Core.DTOs;
using WWTBA.Web.Services;

namespace WWTBA.Web.Controllers;

public class LessonController : Controller
{
    private readonly LessonApiService _lessonApiService;

    public LessonController(LessonApiService lessonApiService)
    {
        _lessonApiService = lessonApiService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _lessonApiService.GetAllAsync());
    }

    public IActionResult Save()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Save(LessonCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            await _lessonApiService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    public async Task<IActionResult> Update(int id)
    {
        LessonDto lesson = await _lessonApiService.GetByIdAsync(id);

        return View(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> Update(LessonDto dto)
    {
        if (ModelState.IsValid)
        {
            await _lessonApiService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        return View(dto);
    }

    public async Task<IActionResult> Remove(int id)
    {
        await _lessonApiService.RemoveAsync(id);

        return RedirectToAction(nameof(System.Index));
    }
}