using Confluent.Kafka;
using System.Text.Json;
using NotificationService.DTOs;

namespace NotificationService.Services
{
    public class KafkaNotificationConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaNotificationConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", // Kafka broker
                GroupId = "notification-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("appointments"); // Topic to consume
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);
                        if (result != null)
                        {
                            var appointment = JsonSerializer.Deserialize<AppointmentMessageDto>(result.Message.Value);
                            if (appointment != null)
                            {
                                // Dummy notification: just print to console
                                Console.WriteLine($"[Notification] New Appointment Created: " +
                                                  $"PatientId={appointment.PatientId}, " +
                                                  $"DoctorId={appointment.DoctorId}, " +
                                                  $"Date={appointment.AppointmentDate}, " +
                                                  $"Status={appointment.Status}");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break; // graceful shutdown
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error consuming Kafka message: {ex.Message}");
                    }
                }
            });
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}