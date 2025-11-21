using NotificationService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<KafkaNotificationConsumer>();
    })
    .Build();

await host.RunAsync();