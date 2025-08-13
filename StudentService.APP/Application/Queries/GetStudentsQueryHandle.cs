

namespace StudentService.APP.Application.Queries
{
    public class GetStudentsQueryHandle : IRequestHandler<GetStudentsQuery, List<StudentResponse>>
    {
        private readonly IStudentRepository _repo;
        public GetStudentsQueryHandle(IStudentRepository repo)
        {
            _repo = repo;
        }
        public Task<List<StudentResponse>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return GetStudents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async Task<List<StudentResponse>> GetStudents()
        {
            var students = await _repo.GetStudents()
                ;
            var result = students.Select(student => new StudentResponse
            {
                Name = student.StudentName,
                DOB = DateOnly.FromDateTime(student.Birthday),
                Gender = student.Gender.ToString(),
                ClassId = student.ClassId
            }).ToList();
            return result;
        }
    }
}
