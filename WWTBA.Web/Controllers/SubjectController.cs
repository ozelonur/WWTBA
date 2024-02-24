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
            List<SubjectWithQuestionCountModel> list = new();
            foreach (SubjectWithLessonDto item in await _subjectApiService.GetSubjectsWithLessonDto())
            {
                SubjectWithQuestionCountModel model = new()
                {
                    QuestionCount = await _subjectApiService.GetQuestionCountOfASubject(item.Id),
                    SubjectWithLessonDto = item
                };
                
                list.Add(model);
            }
            return View(list);
        }
        
        public async Task<IActionResult> ListSubjects(int id)
        {
            return View(await _subjectApiService.Where(id));
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

        [HttpPost]
        public async Task<IActionResult> Update(SubjectUpdateDto dto)
        {
            int id = dto.Id;
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

