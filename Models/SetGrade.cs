using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class SetGrade
{
    public int SetGradeId { get; set; }

    public int? FkProffesorId { get; set; }

    public int FkGradesId { get; set; }

    public int? FkClassId { get; set; }

    public int? FkStudentId { get; set; }

    public virtual Class? FkClass { get; set; }

    public virtual Grade FkGrades { get; set; } = null!;

    public virtual Proffesor? FkProffesor { get; set; }

    public virtual Student? FkStudent { get; set; }
}
