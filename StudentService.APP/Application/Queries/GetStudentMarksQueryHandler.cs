
namespace StudentService.APP.Application.Queries
{
    public class GetStudentMarksQueryHandler : IRequestHandler<GetStudentMarksQuery, IEnumerable<MarkDTO>>
    {
        private readonly IStudentRepository _repo;
        public GetStudentMarksQueryHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<MarkDTO>> Handle(GetStudentMarksQuery request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetStudent(request.studentId);
            var marks = student.Marks.Select(x => new MarkDTO
            {
                SubjectName = x.Subject.SubjectName,
                Value = x.Score
            });
            return marks;
        }
    }
}
