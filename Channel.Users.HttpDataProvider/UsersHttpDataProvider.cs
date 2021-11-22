using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Channel.Users.Domain.DomainEntities.User;
using Channel.Users.Domain.Infrastructure;
using Channel.Users.Domain.Reporting.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Channel.Users.HttpDataProvider
{
    public class UsersHttpDataProvider : IUsersDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsersHttpDataProvider> _logger;
        private readonly HttpDataProviderSettings _settings;

        public UsersHttpDataProvider(IHttpClientFactory httpClientFactory, ILogger<UsersHttpDataProvider> logger,
            IOptions<HttpDataProviderSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _settings = settings?.Value ?? throw new ArgumentException("Please provide the HttpDataProviderSettings", nameof(settings));
        }

        public async Task<IList<User>> GetUsers()   
        {
            var client = _httpClientFactory.CreateClient("HttpDataProvider");

            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_settings.Url)
            });

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"The user data provider returned a non-successful response " +
                                 $"code {response.StatusCode} with body {responseBody}.");

                throw new Exception("There was an error while fetching the user data.");
            }

            var usersData = JsonConvert.DeserializeObject<List<UserData>>(responseBody);

            return usersData?.Select(MapToDomainObject).ToList();
        }

        private User MapToDomainObject(UserData user)
        {
            // Unfortunately init properties are not available in C# 8 yet
            return new User(user.Id, user.First, user.Last, user.Age, ParseGender(user.Gender));
        }

        private static GenderOptions? ParseGender(string gender)
        {
            return gender switch
            {
                "M" => GenderOptions.Male,
                "F" => GenderOptions.Female,
                _ => null
            };
        }
    }
}
