using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;

        public QuestionsController(IQuestionService questionService, IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }

        [ServiceFilter(typeof(NotFoundFilter<Question, QuestionWithAnswersDto>))]
        [HttpGet("[action]/{questionId}")]
        public async Task<IActionResult> GetQuestionWithAnswers(int questionId)
        {
            return CreateActionResult(await _questionService.GetQuestionWithAnswersAsync(questionId));
        }

        [HttpGet("[action]/{userId}/{subjectId}")]
        public async Task<IActionResult> GetUnsolvedQuestionsWithAnswersBySubject(int userId, int subjectId)
        {
            return CreateActionResult(
                await _questionService.GetUnsolvedQuestionsWithAnswersBySubjectAsync(userId, subjectId));
        }
        
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _questionService.GetAllAsync());
        }

        [HttpGet("[action]/{subjectId}")]
        public async Task<IActionResult> GetQuestionsToASingleSubject(int subjectId)
        {
            return CreateActionResult(await _questionService.Where(x => x.SubjectId == subjectId));
        }

        [HttpPost]
        public async Task<IActionResult> Add(QuestionCreateDto question)
        {
            return CreateActionResult(await _questionService.AddAsync(question));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRangeWithAnswers(IEnumerable<QuestionCreateViewModel> models)
        {
            foreach (QuestionCreateViewModel model in models)
            {
                QuestionDto dto = _questionService.AddAsync(model.QuestionCreateDto).Result.Data;
                foreach (AnswerCreateDto answerDto in model.AnswerCreateDtos)
                {
                    answerDto.QuestionId = dto.Id;
                    await _answerService.AddAsync(answerDto);
                }
            }
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK));
        }

        [ServiceFilter(typeof(NotFoundFilter<Question, QuestionDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _questionService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(QuestionUpdateDto dto)
        {
            return CreateActionResult(await _questionService.UpdateAsync(dto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Question, QuestionUpdateDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _questionService.RemoveAsync(id));
        }

        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _questionService.AnyAsync(x => x.Id == id));
        }
    }
}