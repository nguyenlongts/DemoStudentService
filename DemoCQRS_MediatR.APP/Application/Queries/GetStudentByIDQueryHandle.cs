

namespace DemoCQRS_MediatR.APP.Application.Queries
{
    public class GetStudentByIDQueryHandle : IRequestHandler<GetStudentByIDQuery, GetStudentResponse>
    {
        private readonly StudentRepository _repo;
        public GetStudentByIDQueryHandle(StudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<GetStudentResponse> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await GetStudent(request.StudentId);
                if (student == null)
                {
                    return null;
                }
                var result = new GetStudentResponse()
                {
                    Name = student.StudentName,
                    DOB = DateOnly.FromDateTime(student.Birthday),
                    Gender = student.Gender.ToString(),
                    Marks = student.Marks.Select(m => new MarkDTO
                    {
                        SubjectName = m.Subject.SubjectName,
                        Value = m.Score
                    })
                    .ToList()
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
