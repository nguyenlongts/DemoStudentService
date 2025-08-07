using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ModelContext _context;
        public ClassRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task DecrementStudentCount(int id)
        {
            var existClass = await GetById(id);
            if (existClass == null) { return; }
            existClass.DecreaseStudentCount();
        }

        public async Task<Class> GetById(int id)
        {
            var @class = await _context.Classes.FirstOrDefaultAsync(c => c.Classid == id);
            if (@class == null) { return null; }
            return @class;
        }

        public async Task IncrementStudentCount(int id)
        {
            var existClass = await GetById(id);
            if (existClass == null) { return; }
            existClass.IncreaseStudentCount();
        }

        public async Task SaveAsync()
        {   
            await _context.SaveChangesAsync();
        }

        public void Update(Class @class)
        {
            _context.Update(@class);
        }
    }
}
