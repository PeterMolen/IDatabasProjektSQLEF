using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public string? ClassInfo { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
