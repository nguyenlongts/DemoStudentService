namespace DemoCQRS_MediatR.Domain.Entites;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

    public decimal Status { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();


}
