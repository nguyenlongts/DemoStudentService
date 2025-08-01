using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCQRS_MediatR.Domain.Events
{
    public class ConfirmedDeleteStudentEvent:INotification
    {
        public int StudentId { get; }

        public ConfirmedDeleteStudentEvent(int studentId)
        {
            StudentId = studentId;
        }
    }
}
