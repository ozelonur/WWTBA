using WWTBA.Core.DTOs;

namespace WWTBA.Web.Services
{
    public class LessonApiService
    {
        private readonly HttpClient _client;

        public LessonApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<LessonDto>> GetAllAsync()
        {
            CustomResponseDto<List<LessonDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<LessonDto>>>("lessons");
            return response.Data;
        }

        public async Task<LessonDto> AddAsync(LessonCreateDto dto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("lessons", dto);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            CustomResponseDto<LessonDto> responseBody =
                await response.Content.ReadFromJsonAsync<CustomResponseDto<LessonDto>>();
            return responseBody.Data;
        }

        public async Task<LessonDto> GetByIdAsync(int id)
        {
            CustomResponseDto<LessonDto> response =
                await _client.GetFromJsonAsync<CustomResponseDto<LessonDto>>($"lessons/{id}");
            return response.Data;
        }
        
        public async Task<bool> UpdateAsync(LessonDto lessonDto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("lessons", lessonDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"lessons/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

