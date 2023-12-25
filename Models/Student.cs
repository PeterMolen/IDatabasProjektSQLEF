using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentFirstName { get; set; } = null!;

    public string StudentLastName { get; set; } = null!;

    public string? Personal { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
