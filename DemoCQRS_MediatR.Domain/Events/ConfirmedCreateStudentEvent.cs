using DemoCQRS_MediatR.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCQRS_MediatR.Domain.Events
{
    public class ConfirmedCreateStudentEvent:INotification
    {
        public Student Student { get; }
        public ConfirmedCreateStudentEvent(Student student)
        {
            Student = student;
        }
    }
}
