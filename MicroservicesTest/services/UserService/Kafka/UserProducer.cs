using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Config;

namespace UserService.Kafka
{
    public class UserProducer : IUserProducer
    {
        private ProducerConfig _producerConfig;

        public UserProducer(KafkaConfig producerConfig)
        {
            _producerConfig = new ProducerConfig { BootstrapServers = "kafka:9092" };
            Console.WriteLine("added config: " + _producerConfig.BootstrapServers);
        }
        public void produce(string userName)
        {
            
            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery Error: {r.Error.Reason}");
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                var stringValue = "Hello Kafka, with input: " + userName;
                producer.ProduceAsync("user-topic", new Message<Null, string> { Value = stringValue });
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
