using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Options;

namespace TaskTrackerX.TaskApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SettingOptions _settingOptions;

        public UserService(
            IHttpClientFactory httpClientFactory,
            IOptions<SettingOptions> settingOptions)
        {
            _httpClientFactory = httpClientFactory;
            _settingOptions = settingOptions.Value;
        }

        public async Task<ApiResponse<UserDto>> FindByIdAsync(Guid userId)
        {
            var httpClient = _httpClientFactory.CreateClient(_settingOptions.AuthApi.ServiceName);

            HttpResponseMessage response = await httpClient.GetAsync($"api/users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserDto>>(content);

                if (apiResponse == null)
                    throw new JsonException(nameof(apiResponse));

                return apiResponse;
            }

            return new ApiResponse<UserDto>()
            {
                ErrorMessage = new ErrorInfo[] {
                    ErrorDescriber.ServerNotResponding(_settingOptions.AuthApi.ServiceName
                    )}
            };
        }
    }
}
