namespace StudentService.APP.Application.DomainEventHandler
{
    public class ValidateClassWhenStudentClassChangedEventHandler : INotificationHandler<StudentClassChangedEvent>
    {
        private readonly IProducer <Null,string> _producer;
        private readonly IClassRepository _repo;
        public ValidateClassWhenStudentClassChangedEventHandler(IProducer<Null, string> producer, IClassRepository repo)
        {
            _producer = producer;
            _repo = repo;
        }
        public async Task Handle(StudentClassChangedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var newClass = await _repo.GetById(notification.NewClassId);
                if (newClass == null)
                {
                    throw new Exception("New class not found");
                }
                if (newClass.IsCapMax())
                {
                    throw new Exception(
                            $"Class is full. Current: {newClass.StudentCount}, Max: 40");
                }
                await _repo.IncrementStudentCount(notification.NewClassId);
                await _repo.DecrementStudentCount(notification.OldClassId);
                await _repo.SaveAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            var data = new AssignedStudentToNewClassIntegrationEvent(notification.StudentId, notification.OldClassId, notification.NewClassId);
            var msg = new Message<Null, string> { Value = JsonSerializer.Serialize(data) };
            await _producer.ProduceAsync("assign-class-student-topic", msg);
        }
        
    }
}
