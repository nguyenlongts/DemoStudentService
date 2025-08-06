using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.Events
{
    public class StudentRequestedClassChangeEvent : INotification
    {
        public int StudentId { get; private set; }
        public int NewClassId { get;private set; }

        public StudentRequestedClassChangeEvent(int newclassId, int studentId)
        {
            NewClassId = newclassId;
            StudentId = studentId;
        }
    }
}
