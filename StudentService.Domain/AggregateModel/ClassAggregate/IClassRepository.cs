

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
