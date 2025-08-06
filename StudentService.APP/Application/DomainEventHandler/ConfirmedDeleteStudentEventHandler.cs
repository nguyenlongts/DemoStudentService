using StudentService.Domain.AggregateModel.StudentAggregate;
using StudentService.Domain.Events;

namespace StudentService.APP.Application.DomainEventHandler
{
    public class ConfirmedDeleteStudentEventHandler : INotificationHandler<ConfirmedDeleteStudentEvent>
    {

        private readonly IProducer<Null, string> _producer;
        public ConfirmedDeleteStudentEventHandler(IStudentRepository repo, IProducer<Null, string> producer)
        {
            _producer = producer;
        }


        public async Task Handle(ConfirmedDeleteStudentEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var integrationEvent = new StudentDeletedIntegrationEvent(
                    $"Student with ID {notification.StudentId} deleted.",
                    true,
                    notification.ClassId);

                var msg = new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(integrationEvent)
                };

                await _producer.ProduceAsync("student-delete-topic", msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kafka publish failed: {ex.Message}");
            }
        }
    }

}

