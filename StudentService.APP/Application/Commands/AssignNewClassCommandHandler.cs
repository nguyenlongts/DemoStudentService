
namespace StudentService.APP.Application.Commands
{
    public class AssignNewClassCommandHandler : IRequestHandler<AssignNewClassCommand, bool>
    {
        private readonly IStudentRepository _repo;
        private readonly IProducer<Null, string> _producer;
        public AssignNewClassCommandHandler(IStudentRepository repo, IProducer<Null, string> producer)
        {
            _repo = repo;
            _producer = producer;
        }
        public async Task<bool> Handle(AssignNewClassCommand request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetStudent(request.StudentId);
            if (student == null)
            {
                return false;
            }
            if (student.ClassId == request.NewClassId)
            {
                throw new Exception("New class Id is same as current class Id");
            }
            if (request.NewClassId < 1)
            {
                throw new Exception("class Id must greater than 0");
            }
            var @event = new StudentRequestedClassChangeEvent
            (
                request.NewClassId,
                student.StudentId

            );
            await _producer.ProduceAsync("student-request-class-change-topic", new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(@event)
            });
            return true;

        }
    }
}
