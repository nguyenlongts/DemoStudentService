

namespace StudentService.Domain.Events
{
    public class CreatedStudentEvent:INotification
    {
        public Student Student { get; }
        public CreatedStudentEvent(Student student)
        {
            Student = student;
        }
    }
}
