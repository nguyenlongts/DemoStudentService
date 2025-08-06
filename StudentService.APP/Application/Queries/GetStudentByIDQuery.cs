using StudentService.APP.DTOs;

namespace StudentService.APP.Application.Queries
{
    public record GetStudentByIDQuery(int StudentId) : IRequest<StudentResponse>
    {
    }
}
