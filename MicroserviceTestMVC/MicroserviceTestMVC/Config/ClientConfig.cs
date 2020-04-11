using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Config
{
    public class ClientConfig
    {
        public APIGatewayConfig APIGateway { get; set; } = new APIGatewayConfig();
    }
}
