using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WWTBA.Core.DTOs;
using WWTBA.Web.Services;

namespace WWTBA.Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectApiService _subjectApiService;
        private readonly LessonApiService _lessonApiService;

        public SubjectController(SubjectApiService subjectApiService, LessonApiService lessonApiService)
        {
            _subjectApiService = subjectApiService;
            _lessonApiService = lessonApiService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _subjectApiService.GetSubjectsWithLessonDto());
        }

        public async Task<IActionResult> Save()
        {
            List<LessonDto> lessons = await _lessonApiService.GetAllAsync();

            ViewBag.lessons = new SelectList(lessons, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(SubjectCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _subjectApiService.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<LessonDto> lessons = await _lessonApiService.GetAllAsync();

            ViewBag.lessons = new SelectList(lessons, "Id", "Name");
            
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            SubjectUpdateDto subject = await _subjectApiService.GetByIdAsync(id);
            IEnumerable<LessonDto> lessons = await _lessonApiService.GetAllAsync();

            ViewBag.lessons = new SelectList(lessons, "Id", "Name", subject.LessonId);

            return View(subject);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubjectUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _subjectApiService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            
            IEnumerable<LessonDto> lessons = await _lessonApiService.GetAllAsync();

            ViewBag.lessons = new SelectList(lessons, "Id", "Name", dto.LessonId);

            return View(dto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _subjectApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

