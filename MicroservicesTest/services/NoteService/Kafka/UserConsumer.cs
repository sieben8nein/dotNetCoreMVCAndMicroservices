using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NoteService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoteService.Kafka
{
    public class UserConsumer : BackgroundService, IUserConsumer
    {
        private ConsumerConfig _config;
        private INoteRepository _repo;
        public UserConsumer(ConsumerConfig consumerConfig, INoteRepository repo)
        {
            _config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _repo = repo;
        }

        private async Task StartConsumer(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_config)
                 // Note: All handlers are called on the main .Consume thread.
                 .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                 .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                 .SetPartitionsAssignedHandler((c, partitions) =>
                 {
                     Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                     // possibly manually specify start offsets or override the partition assignment provided by
                     // the consumer group by returning a list of topic/partition/offsets to assign to, e.g.:
                     // 
                     // return partitions.Select(tp => new TopicPartitionOffset(tp, externalOffsets[tp]));
                 })
                 .SetPartitionsRevokedHandler((c, partitions) =>
                 {
                     Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                 })
                 .Build())
            {
                consumer.Subscribe("user-topic");

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(stoppingToken);

                            if (consumeResult.IsPartitionEOF)
                            {
                                Console.WriteLine(
                                    $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                                continue;
                            }

                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                            if (consumeResult.Offset % 5 == 0)
                            {
                                // The Commit method sends a "commit offsets" request to the Kafka
                                // cluster and synchronously waits for the response. This is very
                                // slow compared to the rate at which the consumer is capable of
                                // consuming messages. A high performance application will typically
                                // commit offsets relatively infrequently and be designed handle
                                // duplicate messages in the event of failure.
                                try
                                {
                                    consumer.Commit(consumeResult);
                                }
                                catch (KafkaException e)
                                {
                                    Console.WriteLine($"Commit error: {e.Error.Reason}");
                                }
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }

        public void Listen(Action<string> message)
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => StartConsumer(stoppingToken));
            return Task.CompletedTask;
        }
    }
}
