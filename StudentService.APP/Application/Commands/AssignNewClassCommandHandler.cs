
using StudentService.Domain.AggregateModel.ClassAggregate;

namespace StudentService.APP.Application.Commands
{

    public class AssignNewClassCommandHandler : IRequestHandler<AssignNewClassCommand, AssignClassResponse>
    {
        private readonly IStudentRepository _studentRepo;
        public AssignNewClassCommandHandler(
            IStudentRepository studentRepo,
            IClassRepository classRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<AssignClassResponse> Handle(AssignNewClassCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepo.GetStudent(request.StudentId);
            if (student == null)
            {
                return new AssignClassResponse
                {
                    isSuccess = false,
                    Message = "Student not found"
                };
            }

            if (student.ClassId == request.NewClassId)
            {
                return new AssignClassResponse
                {
                    isSuccess = false,
                    Message = "New class id is same as old class id"
                };
            }

            if (request.NewClassId < 1)
            {
                return new AssignClassResponse
                {
                    isSuccess = false,
                    Message = "Class Id must be greater than 0"
                };
            }
            var oldClassId = student.ClassId;

            try
            {
                student.AssignToNewClass(request.NewClassId);
                student.SetAssignedNewClass(student.StudentId, oldClassId, request.NewClassId);
                _studentRepo.Update(student);
                await _studentRepo.SaveEntitiesAsync();
                return new AssignClassResponse
                {
                    isSuccess = true,
                    Message = "Assigned successfully"
                };
            }
            catch (Exception ex)
            {
                return new AssignClassResponse
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

