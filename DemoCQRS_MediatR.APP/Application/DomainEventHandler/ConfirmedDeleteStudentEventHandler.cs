using DemoCQRS_MediatR.APP.Application.IntegrationEvents;
using DemoCQRS_MediatR.Domain.Events;
using System.Text.Json;

namespace DemoCQRS_MediatR.APP.Application.DomainEventHandler
{
    public class ConfirmedDeleteStudentEventHandler : INotificationHandler<ConfirmedDeleteStudentEvent>
    {
        private readonly StudentRepository _repo;
        private readonly IProducer<Null, string> _producer;
        public ConfirmedDeleteStudentEventHandler(StudentRepository repo, IProducer<Null, string> producer)
        {
            _repo = repo;
            _producer = producer;
        }
        public async Task Handle(ConfirmedDeleteStudentEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var msg = new Message<Null, string>
                {

                };

                var isDeleted = await _repo.Delete(notification.StudentId);

                var message = $"Delete student with id: {notification.StudentId} !";
                var status = isDeleted;
                var integrationEvent = new StudentDeletedIntegrationEvent(message, status);
                msg.Value = JsonSerializer.Serialize(integrationEvent);


                await _producer.ProduceAsync("student-delete-topic", msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kafka publish failed: {ex.Message}");
            }
        }
    }
}
