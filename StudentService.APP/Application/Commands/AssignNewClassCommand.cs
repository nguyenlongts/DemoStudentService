namespace StudentService.APP.Application.Commands
{
    public class AssignNewClassCommand:IRequest<AssignClassResponse>
    {
        public int StudentId { get; }

        public int NewClassId { get; }

        public AssignNewClassCommand(int studentId, int newClassId)
        {
            StudentId = studentId;
            NewClassId = newClassId;
        }
    }
}
