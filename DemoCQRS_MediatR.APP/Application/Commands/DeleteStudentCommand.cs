

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public record DeleteStudentCommand(int studentId) : IRequest<bool> { }
}
