namespace AppoinmentService.Kafka
{
    using Confluent.Kafka;
    using System.Text.Json;

    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync<T>(string topic, T message)
        {
            var value = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
            Console.WriteLine($"Message sent to Kafka topic '{topic}'");
        }
    }
}