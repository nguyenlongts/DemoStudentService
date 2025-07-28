using DemoCQRS_MediatR.Domain.Entites;
using DemoCQRS_MediatR.Infrastructure.Scaffold;
using Microsoft.EntityFrameworkCore;

namespace EFCore_B3.Infrastructure.Repository
{
    public class StudentRepository
    {
        private readonly ModelContext _context;
        public StudentRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students.Include(st => st.Marks).FirstOrDefaultAsync(s => s.StudentId == id);
            return student;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students.Include(st => st.Marks).ToListAsync();
        }

    }
}
