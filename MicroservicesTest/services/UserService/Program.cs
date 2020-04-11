using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //    var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            //    Action<DeliveryReport<Null, string>> handler = r =>
            //        Console.WriteLine(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery Error: {r.Error.Reason}");

            //    using (var producer = new ProducerBuilder<Null, string>(config).Build())
            //    {
            //        var stringValue = "Hello Kafka";
            //        for (int i = 0; i < 100; i++)
            //        {
            //            stringValue += i;
            //            producer.ProduceAsync("user-topic", new Message<Null, string> { Value = stringValue });
            //        }
            //        producer.Flush(TimeSpan.FromSeconds(10));
            //    }

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
