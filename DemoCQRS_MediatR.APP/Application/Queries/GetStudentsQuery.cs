using DemoCQRS_MediatR.APP.DTOs;
using MediatR;
namespace DemoCQRS_MediatR.APP.Application.Queries
{
    public class GetStudentsQuery : IRequest<List<GetStudentResponse>>
    {
    }
}
