using StudentService.APP.DTOs;
using StudentService.Domain.AggregateModel.StudentAggregate;

namespace StudentService.APP.Application.Queries
{
    public class GetStudentByIDQueryHandle : IRequestHandler<GetStudentByIDQuery, StudentResponse>
    {
        private readonly IStudentRepository _repo;
        public GetStudentByIDQueryHandle(IStudentRepository repo)
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
                    throw new Exception("Not found");
                }
                var result = new StudentResponse()
                {
                    Name = student.StudentName,
                    DOB = DateOnly.FromDateTime(student.Birthday),
                    Gender = student.Gender.ToString(),
                    Marks = student.Marks.Select(m => new MarkDTO
                    {
                        SubjectName = m.Subject.SubjectName,
                        Value = m.Score
                    })
                    .ToList(),
                    ClassId = student.ClassId
                };
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async Task<Student> GetStudent(int id)
        {
            return await _repo.GetStudent(id);
        }
    }
}
