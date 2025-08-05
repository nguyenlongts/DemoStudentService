using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCQRS_MediatR.Domain.AggregateModel.StudentAggregate
{
    public interface IStudentRepository
    {
        Task<Student> GetStudent(int id);
        void Delete(Student student);

        void Update(Student model);
        void Create(Student model);
        Task<List<Student>> GetStudents();
        Task SaveEntitiesAsync();
    }
}
