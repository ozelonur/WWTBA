using WWTBA.Core.DTOs;

namespace WWTBA.Web.Services
{
    public class SubjectApiService
    {
        private readonly HttpClient _client;

        public SubjectApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<SubjectWithLessonDto>> GetSubjectsWithLessonDto()
        {
            CustomResponseDto<List<SubjectWithLessonDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<SubjectWithLessonDto>>>(
                    "subjects/GetSubjectsWithLesson");

            return response.Data;
        }
        
        public async Task<IEnumerable<SubjectDto>> Where(int lessonId)
        {
            CustomResponseDto<IEnumerable<SubjectDto>> responseDto =
                await _client.GetFromJsonAsync<CustomResponseDto<IEnumerable<SubjectDto>>>(
                    $"subjects/GetSubjectsToASingleLesson/{lessonId}");
            return responseDto.Data;
        }
        

        public async Task<SubjectDto> AddAsync(SubjectCreateDto dto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("subjects", dto);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            CustomResponseDto<SubjectDto> responseBody =
                await response.Content.ReadFromJsonAsync<CustomResponseDto<SubjectDto>>();

            return responseBody.Data;
        }
        
        public async Task<SubjectUpdateDto> GetByIdAsync(int id)
        {
            CustomResponseDto<SubjectUpdateDto> response =
                await _client.GetFromJsonAsync<CustomResponseDto<SubjectUpdateDto>>($"subjects/{id}");
            return response.Data;
        }

        public async Task<int> GetQuestionCountOfASubject(int subjectId)
        {
            CustomResponseDto<int> response =
                await _client.GetFromJsonAsync<CustomResponseDto<int>>(
                    $"subjects/GetQuestionCountOfASubject/{subjectId}");

            return response.Data;
        }
        
        public async Task<bool> UpdateAsync(SubjectUpdateDto subjectDto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("subjects", subjectDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"subjects/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<SubjectDto>> GetAllAsync()
        {
            CustomResponseDto<List<SubjectDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<SubjectDto>>>("subjects");
            return response.Data;
        }
    }
}

