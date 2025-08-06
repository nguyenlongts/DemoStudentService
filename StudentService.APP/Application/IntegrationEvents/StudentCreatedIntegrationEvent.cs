namespace StudentService.APP.Application.IntegrationEvents
{
    public class StudentCreatedIntegrationEvent
    {
        public int StudentId { get;  }
        public string Name { get; } = null!;
        public DateTime DOB { get; }
        public int Gender { get; } 
        public int ClassId { get; }

        public StudentCreatedIntegrationEvent(int studentId, string name, DateTime dOB, int gender, int classId)
        {
            StudentId = studentId;
            Name = name;
            DOB = dOB;
            Gender = gender;
            ClassId = classId;
        }
    }

}
