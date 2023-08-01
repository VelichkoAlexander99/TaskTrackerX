using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
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

            var response = await httpClient.GetAsync($"/api/product");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ApiResponse<UserDto>>(apiContet);

            return resp;
        }
    }
}
