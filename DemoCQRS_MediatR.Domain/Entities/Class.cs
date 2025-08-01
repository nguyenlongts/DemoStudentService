using DemoCQRS_MediatR.Domain.Entities;
using DemoCQRS_MediatR.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DemoCQRS_MediatR.Domain.Entities;

public partial class Class
{
    public int Classid { get; private set; }

    public string? Classname { get; private set; }
    public int StudentCount { get;  set; }
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public Class()
    {

    }
    public Class(int id, string classname)
    {
        Classid = ValidateClassId(id);
        Classname = ValidateClassName(classname);
    }

    private  int ValidateClassId(int classId)
    {
        if (classId <= 0)
            throw new DomainException("ClassId must be greater than 0");

        return classId;
    }
    private  string ValidateClassName( string classname)
    {
        if (string.IsNullOrEmpty(classname) || string.IsNullOrWhiteSpace(classname))
        {
            throw new DomainException("Name is not allowed null");
        }
        if (classname.Length > 30)
        {
            throw new DomainException("Name length must smaller than 30");
        }
        return classname;
    }

}
