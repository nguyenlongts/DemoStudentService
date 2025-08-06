namespace StudentService.APP.Application.Commands
{
    public class AddMarkCommand : IRequest<bool>
    {
        public int StudentId { get; }
        public int SubjectId { get; }
        public int Score { get; }
            
        public AddMarkCommand(int studentId,int subjectId, int score)
        {
            SubjectId = subjectId;
            Score = score;
            StudentId = studentId;
        }

    }
}
