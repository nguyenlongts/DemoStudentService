using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Infrastructure
{
    public class ClassRepository : IClassRepository
    {
        private readonly ModelContext _context;
        public ClassRepository(ModelContext context)
        {
            _context = context;
        }
        public Class GetById(int id)
        {
            return _context.Classes.FirstOrDefault(c => c.Classid == id);
        }

        public void Save()
        {   
            _context.SaveChanges();
        }

        public void Update(Class @class)
        {
            _context.Update(@class);
        }
    }
}
