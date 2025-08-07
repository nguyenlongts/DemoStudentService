
namespace StudentService.APP.Application.Commands
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentRepository _repo;
        public UpdateStudentCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await Update(request);
            if (!isSuccess)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> Update(UpdateStudentCommand request)
        {

            var student = await _repo.GetStudent(request.StudentId);
            if (student == null)
            {
                return false;
            }

            student.UpdateInfo(request.Name, request.DOB, (Gender)request.Gender, (StudentStatus)request.Status);
            _repo.Update(student);
            return true;

        }

    }
}
