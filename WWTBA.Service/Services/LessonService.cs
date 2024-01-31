using AutoMapper;
using Microsoft.AspNetCore.Http;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;

namespace WWTBA.Service.Services
{
    public class LessonService : Service<Lesson, LessonDto>, ILessonService
    {
        private ILessonRepository _lessonRepository;

        public LessonService(IGenericRepository<Lesson> repository, IUnitOfWork unitOfWork, IMapper mapper,
            ILessonRepository lessonRepository) : base(
            repository, unitOfWork, mapper)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<CustomResponseDto<LessonDto>> AddAsync(LessonCreateDto dto)
        {
            Lesson newEntity = _mapper.Map<Lesson>(dto);
            await _lessonRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            LessonDto newDto = _mapper.Map<LessonDto>(newEntity);
            return CustomResponseDto<LessonDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<LessonDto>> UpdateAsync(LessonUpdateDto dto)
        {
            Lesson newEntity = _mapper.Map<Lesson>(dto);
            _lessonRepository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            LessonDto newDto = _mapper.Map<LessonDto>(newEntity);
            return CustomResponseDto<LessonDto>.Success(StatusCodes.Status200OK, newDto);
        }
    }
}