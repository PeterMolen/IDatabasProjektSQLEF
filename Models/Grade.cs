using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Grade
{
    public int GradesId { get; set; }

    public string? GradeSet { get; set; }

    public DateTime? GradeDateSet { get; set; }

    public int? GradesInfo { get; set; }

    public virtual ICollection<SetGrade> SetGrades { get; set; } = new List<SetGrade>();
}
