using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.AggregateModel.StudentAggregate
{
    public interface IClassRepository
    {
        Class GetById(int id);

        void Update(Class @class);

        void Save();
    }

}
