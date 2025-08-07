
using StudentService.Domain.AggregateModel.ClassAggregate;

namespace StudentService.APP.Application.Commands
{

    public class AssignNewClassCommandHandler : IRequestHandler<AssignNewClassCommand, bool>
    {
        private readonly IStudentRepository _studentRepo;
        public AssignNewClassCommandHandler(
            IStudentRepository studentRepo,
            IClassRepository classRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<bool> Handle(AssignNewClassCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepo.GetStudent(request.StudentId);
            if (student == null)
            {
                return false;
            }

            if (student.ClassId == request.NewClassId)
            {

                throw new Exception("New class Id is same as old class id");
            }

            if (request.NewClassId < 1)
            {
                throw new Exception("Class Id must be greater than 0");
            }
            var oldClassId = student.ClassId;

            try
            {
                student.AssignToNewClass(request.NewClassId);
                student.SetAssignedNewClass(student.StudentId, oldClassId, request.NewClassId);
                _studentRepo.Update(student);
                await _studentRepo.SaveEntitiesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
    }
}

