using MediatR;

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class CreateStudentCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Gender { get; set; }

        public int Status { get; set; }

        public CreateStudentCommand(string name, DateTime dob, int gender, int status)
        {
            Name = name;
            DOB = dob;
            Gender = gender;
            Status = status;
        }
    }
}
