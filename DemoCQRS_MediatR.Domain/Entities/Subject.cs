namespace DemoCQRS_MediatR.Domain.Entities;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public decimal Status { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
