using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.AggregateModel.ClassAggregate
{
    public interface IClassRepository
    {
        Task<Class> GetById(int id);

        void Update(Class @class);

        Task SaveAsync();

        Task IncrementStudentCount(int id);
        Task DecrementStudentCount(int id);
    }

}
