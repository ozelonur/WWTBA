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
                    "question/GetQuestionWithAnswers");
            return response.Data;
        }

        public async Task<QuestionDto> AddAsync(QuestionCreateDto dto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("question", dto);
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
                await _client.GetFromJsonAsync<CustomResponseDto<List<QuestionDto>>>("question");
            return response.Data;
        }

        public async Task<QuestionDto> GetByIdAsync(int id)
        {
            CustomResponseDto<QuestionDto> response =
                await _client.GetFromJsonAsync<CustomResponseDto<QuestionDto>>($"question/{id}");
            return response.Data;
        }

        public async Task<bool> UpdateAsync(QuestionUpdateDto dto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("question", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"question/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<QuestionDto>> Where(int subjectId)
        {
            CustomResponseDto<IEnumerable<QuestionDto>> responseDto =
                await _client.GetFromJsonAsync<CustomResponseDto<IEnumerable<QuestionDto>>>(
                    $"question/GetQuestionsToASingleSubject/{subjectId}");
            return responseDto.Data;
        }
    }
}