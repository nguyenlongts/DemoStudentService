using StudentService.Domain.AggregateModel.StudentAggregate;

namespace StudentService.Domain.Events
{
    public class ConfirmedCreateStudentEvent:INotification
    {
        public Student Student { get; }
        public ConfirmedCreateStudentEvent(Student student)
        {
            Student = student;
        }
    }
}
