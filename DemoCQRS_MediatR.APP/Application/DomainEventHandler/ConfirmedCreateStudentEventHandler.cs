using DemoCQRS_MediatR.APP.Application.IntegrationEvents;
using DemoCQRS_MediatR.Domain.Entities;
using DemoCQRS_MediatR.Domain.Events;
using MediatR;
using System.Text.Json;

namespace DemoCQRS_MediatR.APP.Application.DomainEventHandler
{
    public class ConfirmedCreateStudentEventHandler : INotificationHandler<ConfirmedCreateStudentEvent>
    {
        private readonly StudentRepository _repo;
        private readonly IProducer<Null, string> _producer;
        public ConfirmedCreateStudentEventHandler(IProducer<Null, string> producer, StudentRepository repo)
        {
            _producer = producer;
            _repo = repo;
        }

        public async Task Handle(ConfirmedCreateStudentEvent notification, CancellationToken cancellationToken)
        {
            try
            {

                var student = notification.Student;
                var isCreated = await _repo.CreateAsync(student);
                if (isCreated)
                {
                    var integrationEvent = new StudentCreatedIntegrationEvent
                    {
                        StudentId = student.StudentId,
                        Name = student.StudentName,
                        DOB = student.Birthday,
                        Gender = (int)student.Gender,
                        ClassId = student.ClassId
                    };
                    var message = new Message<Null, string>
                    {
                        Value = JsonSerializer.Serialize(integrationEvent)
                    };

                    await _producer.ProduceAsync("student-create-topic", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kafka publish failed: {ex.Message}");
            }
        }
    }


}
