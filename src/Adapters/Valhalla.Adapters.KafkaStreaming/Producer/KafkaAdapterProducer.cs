﻿using Confluent.Kafka;
using System.Text.Json;

namespace Valhalla.Adapters.KafkaStreaming.Producer
{
    public class KafkaAdapterProducer : IKafkaAdapter
    {
        public void Produce(object data)
        {
            var config = new ProducerConfig { BootstrapServers = "127.0.0.1:9092" };
            using var p = new ProducerBuilder<Null, string>(config).Build();
            {
                try
                {
                    var dr = p.ProduceAsync("orders-topic",
                        new Message<Null, string> { Value = JsonSerializer.Serialize(data) });
                }
                catch (ProduceException<Null, string> e)
                {
                    //please implement a better exception :)
                    _ = e.Error.Reason;
                }
            }
        }
    }
}
