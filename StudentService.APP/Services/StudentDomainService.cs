namespace StudentService.APP.Services
{
    public class StudentDomainService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IClassRepository _classRepo;
        public StudentDomainService(IStudentRepository studentRepo, IClassRepository classRepo)
        {
            _studentRepo = studentRepo;
            _classRepo = classRepo;
        }

        public async Task<StudentResponse> GetStudentWithClass(int id)
        {
            var student = await _studentRepo.GetStudent(id);
            var @class = await _classRepo.GetById(id);
            var response = new StudentResponse
            {
                ClassId = @class.ClassId,
                DOB = DateOnly.FromDateTime(student.Birthday),
                Gender = student.Gender.ToString(),
                Name = student.StudentName,
                Message = "true"
            };
            return response;
        }
    }
}
