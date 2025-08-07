namespace StudentService.APP.Application.IntegrationEvents
{
    public class AssignedStudentToNewClassIntegrationEvent
    {
        public int StudentId { get; private set; }
        public int OldClassId { get; private set; }
        public int NewClassId { get; private set; }
        public AssignedStudentToNewClassIntegrationEvent(int studentId, int oldClassId, int newClassId)
        {
            StudentId = studentId;
            OldClassId = oldClassId;
            NewClassId = newClassId;
        }

    }
}
