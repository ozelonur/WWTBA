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
                    "subject/GetSubjectsWithLesson");

            return response.Data;
        }

        public async Task<SubjectDto> AddAsync(SubjectCreateDto dto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("subject", dto);

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
                await _client.GetFromJsonAsync<CustomResponseDto<SubjectUpdateDto>>($"subject/{id}");
            return response.Data;
        }
        
        public async Task<bool> UpdateAsync(SubjectUpdateDto lessonDto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("subject", lessonDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"subject/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<SubjectDto>> GetAllAsync()
        {
            CustomResponseDto<List<SubjectDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<SubjectDto>>>("subject");
            return response.Data;
        }
    }
}

