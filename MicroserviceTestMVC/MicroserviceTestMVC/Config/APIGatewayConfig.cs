using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Config
{
    public class APIGatewayConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(Host) || Port == 0)
                {
                    Console.WriteLine("error, port is :" + Port);
                    return $@"http://{Host}:{Port}";
                }
                else
                {
                    Console.WriteLine("success, host and port found to be: " + Host + ":" + Port);
                    return $@"http://{Host}:{Port}";
                }
            }
        }
    }
}
