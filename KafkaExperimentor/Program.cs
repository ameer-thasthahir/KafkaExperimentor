using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Collections.Generic;

namespace KafkaExperimentor
{
    class Program
    {
        static void Main(string[] args)
        {
            string kafkaEndpoint = "localhost:9092";

            // The Kafka topic we'll be usingB
            string kafkaTopic = "javainuse-topic";

            // Create the producer configuration
            var producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };

            // Create the producer
            using (var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
            {
                // Send 10 messages to the topic
                for (int i = 0; i < 10; i++)
                {
                    var message = $"Event {i}";
                    var result = producer.ProduceAsync(kafkaTopic, null, message).GetAwaiter().GetResult();
                    Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}");
                }
            }

            Console.Read();
        }
    }
}
