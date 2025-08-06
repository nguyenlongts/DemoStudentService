

namespace StudentService.APP.Application.DomainEventHandler
{
    public class CreatedStudentEventHandler : INotificationHandler<CreatedStudentEvent>
    {

        private readonly IProducer<Null, string> _producer;
        public CreatedStudentEventHandler(IProducer<Null, string> producer, IStudentRepository repo)
        {
            _producer = producer;

        }

        public async Task Handle(CreatedStudentEvent notification, CancellationToken cancellationToken)
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
            (
                student.StudentId,
                student.StudentName,
                student.Birthday,
                (int)student.Gender,
                student.ClassId
                );
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