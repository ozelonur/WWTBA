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
            HttpResponseMessage response = await _client.PostAsJsonAsync("answer/SaveAll", dtos);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            CustomResponseDto<List<AnswerDto>> responseBody =
                await response.Content.ReadFromJsonAsync<CustomResponseDto<List<AnswerDto>>>();
            return responseBody.Data;
        }
    }
}

