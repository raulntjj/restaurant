using System.Text;
using ItemService.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ItemService.RabbitMqClient;

public class RabbitMqSubscriber : BackgroundService
{
  private readonly IConfiguration _configuration;
  private readonly string _queue;
  private readonly IConnection _connection;
  private readonly IModel _channel;
  private readonly IEventProcess _eventProcess;

  public RabbitMqSubscriber(IConfiguration configuration)
  {
    _configuration = configuration;
    _connection =
        new ConnectionFactory()
        {
          HostName =
            _configuration["RabbitMqHost"],
          Port =
            Int32.Parse(_configuration["RabbitMqPort"])
        }.CreateConnection();

    _channel = _connection.CreateModel();

    _channel.ExchangeDeclare(
      exchange: "trigger",
      type: ExchangeType.Fanout
    );
    _queue = _channel.QueueDeclare().QueueName;

    _channel.QueueBind(
      queue: _queue,
      exchange: "trigger",
      routingKey: ""
    );
  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var consumer =
      new EventingBasicConsumer(_channel);

    consumer.Received += (ModuleHandle, EventArgs) =>
    {
      ReadOnlyMemory<byte> body = EventArgs.Body;
      string? message = Encoding.UTF8.GetString(body.ToArray());
      _eventProcess.Process(message);
    };

    _channel.BasicConsume(
      queue: _queue,
      autoAck: true,
      consumer: consumer
    );

    return Task.CompletedTask;
  }
}
