namespace StudentService.APP.Application.Commands
{
    public class AddMarkCommandHandler : IRequestHandler<AddMarkCommand,bool>
    {
        private readonly IStudentRepository _repo;
        public AddMarkCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(AddMarkCommand request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetStudent(request.StudentId);
            if (student == null) { return false; }
            student.AddMark(request.SubjectId, request.Score);
            _repo.Update(student);

            await _repo.SaveEntitiesAsync();
            return true;
        }
    }
}
