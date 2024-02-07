using WWTBA.Core.DTOs;

namespace WWTBA.Web.Services
{
    public class AnswerApiService
    {
        private readonly HttpClient _client;

        public AnswerApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AnswerDto>> AddRangeAsync(List<AnswerCreateDto> dtos)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("answers/SaveAll", dtos);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            CustomResponseDto<List<AnswerDto>> responseBody =
                await response.Content.ReadFromJsonAsync<CustomResponseDto<List<AnswerDto>>>();
            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(AnswerUpdateDto dto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("answers", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<AnswerUpdateDto>> Where(int questionId)
        {
            CustomResponseDto<IEnumerable<AnswerUpdateDto>> responseDto =
                await _client.GetFromJsonAsync<CustomResponseDto<IEnumerable<AnswerUpdateDto>>>(
                    $"answers/GetAnswersToASingleQuestion/{questionId}");
            return responseDto.Data;
        }
    }
}