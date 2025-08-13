using StudentService.APP.DTOs;
using StudentService.Domain.AggregateModel.StudentAggregate;
using StudentService.APP.Services;

namespace StudentService.APP.Application.Queries
{
    public class GetStudentByIDQueryHandle : IRequestHandler<GetStudentByIDQuery, StudentResponse>
    {
        private readonly StudentDomainService _repo;
        public GetStudentByIDQueryHandle(StudentDomainService repo)
        {
            _repo = repo;
        }
        public async Task<StudentResponse> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await GetStudent(request.StudentId);
                if (student == null)
                {
                    return null;
                }
                return student;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<StudentResponse> GetStudent(int id)
        {
            return await _repo.GetStudentWithClass(id);
        }
    }
}
