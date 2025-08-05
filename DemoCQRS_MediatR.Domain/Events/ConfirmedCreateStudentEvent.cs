using DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate;

namespace DemoCQRS_MediatR.Domain.Events
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
