
using Microsoft.Extensions.DependencyInjection;
using System;

namespace StudentService.APP.Application.DomainEventHandler
{
    public class ConfirmedAssignNewClassEventHandler : INotificationHandler<StudentRequestedClassChangeEvent>
    {
        private readonly IConsumer <Null,string> _consumer;
        private readonly IStudentRepository _repo;
        public ConfirmedAssignNewClassEventHandler(IConsumer<Null, string> consumer, IStudentRepository repo)
        {
            _consumer = consumer;
            _repo = repo;
        }
        public async Task Handle(StudentRequestedClassChangeEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                _consumer.Subscribe("student-change-class-response-topic");
                while (!cancellationToken.IsCancellationRequested)
                {
                    await ConsumeStudentRequestedClassChangeEventResponse(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kafka publish failed: {ex.Message}");
            }
        }
        public async Task ConsumeStudentRequestedClassChangeEventResponse(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var messageValue = consumeResult.Message.Value;
                var msg = JsonSerializer.Deserialize<object>(messageValue);

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
