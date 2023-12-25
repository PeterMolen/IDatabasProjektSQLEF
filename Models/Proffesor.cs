using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Proffesor
{
    public int ProffesorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime? ProffStartDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Occupation> Occupations { get; set; } = new List<Occupation>();

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
