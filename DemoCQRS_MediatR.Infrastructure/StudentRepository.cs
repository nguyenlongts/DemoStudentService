using DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate;
using DemoCQRS_MediatR.Infrastructure;
using DemoCQRS_MediatR.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace EFCore_B3.Infrastructure.Repository
{
    public class StudentRepository:IStudentRepository
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
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return null;
            }
            student = await _context.Students
                .Include(st => st.Marks)
                    .ThenInclude(m => m.Subject)
                .Include(c => c.Class)
                .FirstOrDefaultAsync(s => s.StudentId == id);
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
                .Include(c => c.Class)
                .ToListAsync();
        }
        public async Task SaveEntitiesAsync()
        {
            await _mediator.DispatchDomainEventAsync(_context);
            await _context.SaveChangesAsync();
        }

    }
}
