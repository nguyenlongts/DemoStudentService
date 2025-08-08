namespace StudentService.APP.Application.Queries
{
    public record GetStudentMarksQuery(int studentId):IRequest<IEnumerable<MarkDTO>>
    {
    }
}
