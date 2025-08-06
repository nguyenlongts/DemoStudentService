namespace StudentService.Domain.Events
{
    public class DeletedStudentEvent : INotification
    {
        public int StudentId { get; }
        public int ClassId { get; }

        public DeletedStudentEvent(int studentId, int classId)
        {
            StudentId = studentId;
            ClassId = classId;
        }
    }

}
