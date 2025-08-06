namespace StudentService.Domain.Events
{
    public class ConfirmedDeleteStudentEvent : INotification
    {
        public int StudentId { get; }
        public int ClassId { get; }

        public ConfirmedDeleteStudentEvent(int studentId, int classId)
        {
            StudentId = studentId;
            ClassId = classId;
        }
    }

}
