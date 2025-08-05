using DemoCQRS_MediatR.APP.Application.IntegrationEvents;
using DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate;
using DemoCQRS_MediatR.Domain.Events;
using MediatR;
using System.Text.Json;

namespace DemoCQRS_MediatR.APP.Application.DomainEventHandler
{
    public class ConfirmedCreateStudentEventHandler : INotificationHandler<ConfirmedCreateStudentEvent>
    {

        private readonly IProducer<Null, string> _producer;
        public ConfirmedCreateStudentEventHandler(IProducer<Null, string> producer, StudentRepository repo)
        {
            _producer = producer;

        }

        public async Task Handle(ConfirmedCreateStudentEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var student = notification.Student;
                var msg = CreateMessage(student);
                await KafkaPublish("student-create-topic", msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kafka publish failed: {ex.Message}");
            }
        }

        public Message<Null, string> CreateMessage(Student student)
        {
            var msg = new StudentCreatedIntegrationEvent
            {
                StudentId = student.StudentId,
                Name = student.StudentName,
                DOB = student.Birthday,
                Gender = (int)student.Gender,
                ClassId = student.ClassId
            };
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(msg)
            };
            return message;
        }


        public async Task KafkaPublish(string topic, Message<Null, string> msg)
        {
            await _producer.ProduceAsync(topic, msg);
        }
    }
}