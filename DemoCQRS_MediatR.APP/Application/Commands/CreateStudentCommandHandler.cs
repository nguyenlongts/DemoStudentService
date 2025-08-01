using DemoCQRS_MediatR.Domain.Events;

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, GetStudentResponse>
    {

        private readonly IPublisher _publisher;

        public CreateStudentCommandHandler(
            StudentRepository repo,
            IPublisher publisher)
        { 
            _publisher = publisher;
        }

        public async Task<GetStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student(request.Name, request.DOB, (Gender)request.Gender, request.ClassId);
            
                await _publisher.Publish(new ConfirmedCreateStudentEvent(student)); 
           
            return new GetStudentResponse { ClassId = request.ClassId, Name = request.Name, DOB = DateOnly.FromDateTime(request.DOB), Gender = request.Gender.ToString() };
        }
    }
}

