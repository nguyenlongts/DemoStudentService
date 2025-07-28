using DemoCQRS_MediatR.Domain;
using DemoCQRS_MediatR.Domain.Entites;
using EFCore_B3.Infrastructure.Repository;
using MediatR;

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, bool>
    {
        private readonly StudentRepository _repo;
        public CreateStudentCommandHandler(StudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await Create(request);
            if (!isSuccess)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> Create(CreateStudentCommand request)
        {
            var student = new Student()
            {
                StudentName = request.Name,
                Birthday = request.DOB,
                Gender = (Gender)request.Gender,
                Status = request.Status
            };
            return await _repo.CreateAsync(student);
        }

    }
}
