using EFCore_B3.Infrastructure.Repository;
using MediatR;

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly StudentRepository _repo;
        public DeleteStudentCommandHandler(StudentRepository repo)
        {
            _repo = repo;
        }
        public Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {

            var isSuccess = Remove(request.studentId);
            return isSuccess;

        }

        private async Task<bool> Remove(int id)
        {
            return await _repo.Delete(id);
        }
    }
}
