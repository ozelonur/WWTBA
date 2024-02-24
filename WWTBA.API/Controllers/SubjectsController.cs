using Microsoft.AspNetCore.Mvc;
using WWTBA.API.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.API.Controllers
{
    public class SubjectsController : CustomBaseController
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjectsWithLesson()
        {
            return CreateActionResult(await _service.GetSubjectsWithDto());
        }
        
        [HttpGet("[action]/{lessonId}")]
        public async Task<IActionResult> GetSubjectsToASingleLesson(int lessonId)
        {
            return CreateActionResult(await _service.Where(x => x.LessonId == lessonId));
        }

        [HttpGet("[action]/{subjectId}")]
        public async Task<IActionResult> GetQuestionCountOfASubject(int subjectId)
        {
            return CreateActionResult(await _service.GetQuestionCountOfASubject(subjectId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<Subject, SubjectDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubjectUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Subject, SubjectDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
        
        [HttpGet("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _service.AnyAsync(x => x.Id == id));
        }
    }
}