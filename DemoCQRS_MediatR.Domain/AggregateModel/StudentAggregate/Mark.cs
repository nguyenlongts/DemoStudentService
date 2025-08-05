namespace DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate;

public partial class Mark
{
    public int StudentId { get; private set; }

    public int SubjectId { get; private set; }

    public int Score { get; private set; }

    public DateTime CreateDate { get; private set; }
    [JsonIgnore]
    public virtual Student Student { get; set; } = null!;
    [JsonIgnore]
    public virtual Subject Subject { get; set; } = null!;

    public Mark() { }   
    public Mark(int studentId, int subjectId, int score)
    {
        StudentId = ValidateId(studentId);
        SubjectId = ValidateId(subjectId);
        CreateDate = DateTime.UtcNow;
        Score = ValidateScore(score);
    }

    private int ValidateId (int id)
    {
        if (id < 1)
        {
            throw new Exception("Id must greater > 0");
        }
        return id;
    }

    private int ValidateScore (int score)
    {
        if (score < 0 || score > 10) {
            throw new Exception("Score in range 0 to 10");
        };
        return score;
    }
}
