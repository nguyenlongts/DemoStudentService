using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.Events
{
    public class StudentClassChangedEvent : INotification
    {
        public int StudentId { get; private set; }
        public int NewClassId { get;private set; }
        public int OldClassId { get; private set; }
        public StudentClassChangedEvent( int studentId, int oldClassId,int newclassId)
        {
            NewClassId = newclassId;
            StudentId = studentId;
            OldClassId = oldClassId;
        }
    }
}
