using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WWTBA.Core.DTOs;
using WWTBA.Web.Services;

namespace WWTBA.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionApiService _questionApiService;
        private readonly AnswerApiService _answerApiService;
        private readonly SubjectApiService _subjectApiService;

        public QuestionController(QuestionApiService questionApiService, AnswerApiService answerApiService,
            SubjectApiService subjectApiService)
        {
            _questionApiService = questionApiService;
            _answerApiService = answerApiService;
            _subjectApiService = subjectApiService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _questionApiService.GetAllAsync());
        }

        public async Task<IActionResult> ListQuestions(int id)
        {
            return View(await _questionApiService.Where(id));
        }

        public async Task<IActionResult> Save()
        {
            List<SubjectDto> subjects = await _subjectApiService.GetAllAsync();
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionDto questionDto = await _questionApiService.AddAsync(model.QuestionCreateDto);
                foreach (AnswerCreateDto dto in model.AnswerCreateDtos)
                {
                    dto.QuestionId = questionDto.Id;
                }
                await _answerApiService.AddRangeAsync(model.AnswerCreateDtos);
                return RedirectToAction(nameof(Index));
            }
            List<SubjectDto> subjects = await _subjectApiService.GetAllAsync();
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View();
        }
    }
}