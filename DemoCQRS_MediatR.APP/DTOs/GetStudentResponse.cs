using DemoCQRS_MediatR.Domain.Entites;

namespace DemoCQRS_MediatR.APP.DTOs
{
    public record GetStudentResponse
    {
        public string Name { get; set; }

        public DateOnly DOB { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();

    }
}
