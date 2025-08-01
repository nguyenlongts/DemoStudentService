
using DemoCQRS_MediatR.Domain.Entities;
using DemoCQRS_MediatR.Domain.Events;
using MediatR;

namespace DemoCQRS_MediatR.APP.Application.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IPublisher _publisher;
        public DeleteStudentCommandHandler(IPublisher publisher)
        {

            _publisher = publisher;
        }
        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            
            await _publisher.Publish(new ConfirmedDeleteStudentEvent(request.studentId));

        }

        
    }
}
