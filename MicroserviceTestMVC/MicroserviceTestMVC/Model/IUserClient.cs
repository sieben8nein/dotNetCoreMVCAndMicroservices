using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Model
{
    public interface IUserClient
    {
        public Task<IEnumerable<User>> GetUsers();
    }
}
