using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Model
{
    public interface IServiceClient
    {
        public Task<IList> Get();
    }
}
