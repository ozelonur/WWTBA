using WWTBA.Core.DTOs;

namespace WWTBA.Web.Services
{
    public class QuestionApiService
    {
        private readonly HttpClient _client;

        public QuestionApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<QuestionWithAnswersDto>> GetQuestionWithAnswersDto()
        {
            CustomResponseDto<List<QuestionWithAnswersDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<QuestionWithAnswersDto>>>(
                    "questions/GetQuestionWithAnswers");
            return response.Data;
        }

        public async Task<QuestionDto> AddAsync(QuestionCreateDto dto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("questions", dto);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            CustomResponseDto<QuestionDto> responseBody =
                await response.Content.ReadFromJsonAsync<CustomResponseDto<QuestionDto>>();
            return responseBody.Data;
        }

        public async Task<List<QuestionDto>> GetAllAsync()
        {
            CustomResponseDto<List<QuestionDto>> response =
                await _client.GetFromJsonAsync<CustomResponseDto<List<QuestionDto>>>("questions");
            return response.Data;
        }

        public async Task<QuestionUpdateDto> GetByIdAsync(int id)
        {
            CustomResponseDto<QuestionUpdateDto> response =
                await _client.GetFromJsonAsync<CustomResponseDto<QuestionUpdateDto>>($"questions/{id}");
            return response.Data;
        }

        public async Task<bool> UpdateAsync(QuestionUpdateDto dto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("questions", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"questions/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<QuestionDto>> Where(int subjectId)
        {
            CustomResponseDto<IEnumerable<QuestionDto>> responseDto =
                await _client.GetFromJsonAsync<CustomResponseDto<IEnumerable<QuestionDto>>>(
                    $"questions/GetQuestionsToASingleSubject/{subjectId}");
            return responseDto.Data;
        }
    }
}