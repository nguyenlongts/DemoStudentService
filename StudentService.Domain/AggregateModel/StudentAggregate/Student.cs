using StudentService.Domain;



namespace StudentService.Domain.AggregateModel.StudentAggregate;

public class Student:BaseEntity,IAggregateRoot

{
    private readonly List<Mark> _marks = new();

    public int StudentId { get; private set; }
    public string StudentName { get; private set; } = null!;
    public DateTime Birthday { get; private set; }
    public Gender Gender { get; private set; }
    public StudentStatus Status { get; private set; }
    public int ClassId { get; private set; }
    public  Class? Class { get; private set; }
    public  IReadOnlyCollection<Mark> Marks => _marks.AsReadOnly();
    private Student() { }

    public Student(string studentName, DateTime birthday, Gender gender, int classId)
    {
        StudentName = ValidateName(studentName);
        Birthday = ValidateDOB(birthday);
        Gender = gender;
        ClassId = ValidateClassId(classId);
        Status = StudentStatus.Active;

      
    }
    public static Student Create(string studentName, DateTime birthday, Gender gender, int classId)
    {
        return new Student(studentName, birthday, gender, classId);
        
    }
    public void UpdateInfo(string name, DateTime birthday, Gender gender, StudentStatus status)
    {
        StudentName = ValidateName(name);
        Birthday = ValidateDOB(birthday);
        Gender = gender;
        Status = status;
    }
    public void SetStudentDeleted()
    {
        var @event = new ConfirmedDeleteStudentEvent(StudentId,ClassId);
        AddDomainEvent(@event);
    }
    public void SetStudentCreated()
    {
        AddDomainEvent(new ConfirmedCreateStudentEvent(this));
    }
    private static int ValidateClassId(int classId)
    {
        if (classId <= 0)
            throw new DomainException("ClassId must be greater than 0");

        return classId;
    }
    public void AddMark(int subjectId,int score)
    {
        _marks.Add(new Mark(StudentId, subjectId, score));
    }
    private DateTime ValidateDOB (DateTime dob)
    {
        if (dob.Date > DateTime.Now.Date)
        {
            throw new DomainException("Invalid birth day");
        }
        return dob;
    }

    private string ValidateName(string Name)
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new DomainException("Name is not allowed null");
        }
        if (Name.Length > 30) {
            throw new DomainException("Name length must smaller than 30");
        }
        return Name;
    }
    public void AssignToNewClass(int classId)
    {
        if (classId <= 0)
            throw new DomainException("ClassId must be greater than 0");
        ClassId=classId;
    }
   
}
public enum StudentStatus
{
    Active = 1,
    Inactive = 0,
    Suspended = -1
}
public enum Gender
{
    Male = 0,
    Female = 1
}