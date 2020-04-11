using MicroserviceTestMVC.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Model
{
    public class UserClient : IUserClient
    {
        private static HttpClient _client;

        public UserClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> _users = Array.Empty<User>();
            var request = new HttpRequestMessage(HttpMethod.Get,
            _client.BaseAddress + "user");
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(responseStream);
            }
            return _users;
        }
    }
}
