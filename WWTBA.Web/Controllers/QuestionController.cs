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
                
                List<SubjectDto> subjectsValue = await _subjectApiService.GetAllAsync();
                ViewBag.subjects = new SelectList(subjectsValue, "Id", "Name");

                return RedirectToAction(nameof(Save));
            }

            List<SubjectDto> subjects = await _subjectApiService.GetAllAsync();
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            QuestionUpdateDto question = await _questionApiService.GetByIdAsync(id);
            IEnumerable<AnswerUpdateDto> answers = await _answerApiService.Where(id);
            QuestionUpdateViewModel model = new()
                { QuestionUpdateDto = question, AnswerUpdateDtos = answers.ToList() };
            IEnumerable<SubjectDto> subjects = await _subjectApiService.GetAllAsync();

            ViewBag.subjects = new SelectList(subjects, "Id", "Name", question.SubjectId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(QuestionUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _questionApiService.UpdateAsync(model.QuestionUpdateDto);
                foreach (AnswerUpdateDto dto in model.AnswerUpdateDtos)
                {
                    dto.QuestionId = model.QuestionUpdateDto.Id;
                    await _answerApiService.UpdateAsync(dto);
                }

                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SubjectDto> subjects = await _subjectApiService.GetAllAsync();

            ViewBag.subjects = new SelectList(subjects, "Id", "Name", model.QuestionUpdateDto.SubjectId);

            return View(model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            int subjectId = (await _questionApiService.GetByIdAsync(id)).SubjectId;

            await _questionApiService.RemoveAsync(id);

            IEnumerable<AnswerUpdateDto> answers = await _answerApiService.Where(id);

            foreach (AnswerUpdateDto answer in answers)
            {
                await _answerApiService.RemoveAsync(answer.Id);
            }

            IEnumerable<QuestionDto> dto = await _questionApiService.Where(subjectId);

            return dto.ToList().Count > 0
                ? RedirectToAction(nameof(ListQuestions))
                : RedirectToAction(nameof(Index), "Subject");
        }
    }
}