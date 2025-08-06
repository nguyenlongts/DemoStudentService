namespace StudentService.APP.Application.Commands
{
    public record DeleteStudentCommand(int studentId) : IRequest<bool> { }
}
