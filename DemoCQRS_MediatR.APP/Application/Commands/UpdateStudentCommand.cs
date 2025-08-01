
namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class UpdateStudentCommand : IRequest<bool>
    {

        public int StudentId { get; set; }
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Gender { get; set; }

        public int Status { get; set; }

        public UpdateStudentCommand(int studentId, string name, DateTime dOB, int gender, int status)
        {
            StudentId = studentId;
            Name = name;
            DOB = dOB;
            Gender = gender;
            Status = status;
        }
    }
}
