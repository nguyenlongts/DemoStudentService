using DemoCQRS_MediatR.Domain.Events;
using DemoCQRS_MediatR.Domain.Exceptions;
using System.Text.Json.Serialization;

namespace DemoCQRS_MediatR.Domain.Entities;

public class Student:BaseEntity

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

      AddDomainEvent(new ConfirmedCreateStudentEvent(this));
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

    private static int ValidateClassId(int classId)
    {
        if (classId <= 0)
            throw new DomainException("ClassId must be greater than 0");

        return classId;
    }

    private static DateTime ValidateDOB (DateTime dob)
    {
        if (dob.Date > DateTime.Now.Date)
        {
            throw new DomainException("Invalid birth day");
        }
        return dob;
    }

    private static string ValidateName(string Name)
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
    public void AssignToClass(Class newclass)
    {
        if (newclass.Classid <= 0)
            throw new DomainException("ClassId must be greater than 0");

        if (newclass == null )
            throw new DomainException("Class not found or mismatched");

        this.Class.StudentCount -= 1;
        ClassId = newclass.Classid;
        Class = newclass;
        newclass.StudentCount += 1;
    }
    public void SetStudentRemove()
    {
        var @event = new ConfirmedDeleteStudentEvent(this.StudentId);
        AddDomainEvent(@event);
    }
}
public enum StudentStatus
{
    Active = 1,
    Inactive = 0,
    Suspended = -1
}