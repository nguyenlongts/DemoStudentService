using StudentService.APP.DTOs;

namespace StudentService.APP.Application.Commands
{
    public class CreateStudentCommand : IRequest<StudentResponse>
    {
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public int Gender { get; set; }

        public int Status { get; set; }

        public int ClassId { get; set; }
        public CreateStudentCommand(string name, DateTime dob, int gender, int status,int classid)
        {
            Name = name;
            DOB = dob;
            Gender = gender;
            Status = status;
            ClassId = classid;
        }
    }
}
