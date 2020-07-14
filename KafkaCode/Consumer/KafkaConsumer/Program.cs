﻿using Confluent.Kafka;
using System;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "grid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("test");

                while (true)
                {
                    var cr = consumer.Consume();


                    Console.WriteLine(cr.Message.Value);
                }
                Console.ReadLine();
            }

        }
    }
}
