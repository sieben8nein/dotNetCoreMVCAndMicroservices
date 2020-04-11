using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceTestMVC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MicroserviceTestMVC.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserClient _userClient;

        public IndexModel(ILogger<IndexModel> logger, IUserClient userClient)
        {
            _logger = logger;
            _userClient = userClient;
        }

        public async void OnGet()
        {
            var users = await _userClient.GetUsers();
            foreach (var user in users)
                Console.WriteLine(user.Name + " ------------------------------------------------------------------------------------------------------");
        }
    }
}
