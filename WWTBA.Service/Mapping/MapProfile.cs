using AutoMapper;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;

namespace WWTBA.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<LessonCreateDto, Lesson>();
            CreateMap<SubjectCreateDto, Subject>();
            CreateMap<LessonUpdateDto, Lesson>();
            CreateMap<SubjectUpdateDto, Subject>();
            CreateMap<Subject, SubjectWithLessonDto>();
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();
            CreateMap<AnswerCreateDto, Answer>();
            CreateMap<AnswerUpdateDto, Answer>();
            CreateMap<Question, QuestionWithAnswersDto>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserAnswer, UserAnswerDto>().ReverseMap();
            CreateMap<User, UserWithAnswersDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserAnswerCreateDto, UserAnswer>();
            CreateMap<UserAnswerUpdateDto, UserAnswer>();
        }
    }
}

