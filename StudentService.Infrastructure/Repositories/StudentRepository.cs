namespace StudentService.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMediator _mediator;
        private readonly ModelContext _context;
        public StudentRepository(ModelContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(st => st.Marks)
                    .ThenInclude(m => m.Subject)
                .FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
            {
                return null;
            }

            return student;
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public void Update(Student model)
        {
            _context.Students.Update(model);

        }
        public void Create(Student model)
        {
            _context.Students.Add(model);
        }
        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students
                .Include(st => st.Marks)
                    .ThenInclude(m => m.Subject)
                .ToListAsync();
        }
        public async Task SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventAsync(_context);
            await _context.SaveChangesAsync();
            
            
        }

    }
}
