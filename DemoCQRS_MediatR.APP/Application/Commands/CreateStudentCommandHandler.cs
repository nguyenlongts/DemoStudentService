using DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, GetStudentResponse>
{
    private readonly IMediator _mediator;
    private readonly StudentRepository _repo;

    public CreateStudentCommandHandler(StudentRepository repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    public async Task<GetStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(request.Name, request.DOB, (Gender)request.Gender, request.ClassId);
        student.SetStudentCreated();

        _repo.Create(student);
        await _repo.SaveEntitiesAsync();

        return new GetStudentResponse
        {
            ClassId = student.ClassId,
            Name = student.StudentName,
            DOB = DateOnly.FromDateTime(student.Birthday),
            Gender = student.Gender.ToString()
        };
    }
}
