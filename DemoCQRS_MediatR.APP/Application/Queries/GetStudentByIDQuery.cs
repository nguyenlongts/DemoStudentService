

namespace DemoCQRS_MediatR.APP.Application.Queries
{
    public record GetStudentByIDQuery(int StudentId) : IRequest<GetStudentResponse>
    {
    }
}
