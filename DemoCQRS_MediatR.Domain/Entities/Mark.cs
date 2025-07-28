using System.Text.Json.Serialization;

namespace DemoCQRS_MediatR.Domain.Entites;

public partial class Mark
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int Score { get; set; }

    public DateTime CreateDate { get; set; }
    [JsonIgnore]
    public virtual Student Student { get; set; } = null!;
    [JsonIgnore]
    public virtual Subject Subject { get; set; } = null!;
}
