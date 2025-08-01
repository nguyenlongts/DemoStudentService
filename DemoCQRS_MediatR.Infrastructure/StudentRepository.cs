using DemoCQRS_MediatR.Domain.Entities;
using DemoCQRS_MediatR.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace EFCore_B3.Infrastructure.Repository
{
    public class StudentRepository
    {
        private readonly IMediator _mediator;
        private readonly ModelContext _context;
        public StudentRepository(ModelContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(st => st.Marks)
                    .ThenInclude(m => m.Subject)
                .Include(c => c.Class)
                .FirstOrDefaultAsync(s => s.StudentId == id);
            return student;
        }

        public async Task<bool> Delete(int id)
        {
            var student = await GetStudent(id);
            if (student == null) { return false; }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public void Update(Student model)
        {
            _context.Students.Update(model);
            _context.SaveChanges();
        }
        public async Task<bool> CreateAsync(Student model)
        {
            try
            {
                await _context.Students.AddAsync(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { Console.Write(ex.ToString()); return false; }
            return true;

        }
        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students
                .Include(st => st.Marks)
                    .ThenInclude(m => m.Subject)
                .Include(c => c.Class)
                .ToListAsync();
        }



    }
}
