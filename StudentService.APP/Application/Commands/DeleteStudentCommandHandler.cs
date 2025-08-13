

namespace StudentService.APP.Application.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IPublisher _publisher;
        private readonly IStudentRepository _repo;
        public DeleteStudentCommandHandler(IPublisher publisher, IStudentRepository repo)
        {

            _publisher = publisher;
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetStudent(request.studentId);
            if (student == null) return false;
            student.SetStudentDeleted();
            _repo.Delete(student);
            await _repo.SaveEntitiesAsync();

            return true;
        }



    }
}
