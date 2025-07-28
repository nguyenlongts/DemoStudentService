using DemoCQRS_MediatR.APP.DTOs;
using MediatR;

namespace DemoCQRS_MediatR.APP.Application.Queries
{
    public record GetStudentByIDQuery(int StudentId) : IRequest<GetStudentResponse>
    {
    }
}
