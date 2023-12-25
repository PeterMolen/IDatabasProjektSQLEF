using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int FkStudentId { get; set; }

    public int FkClassId { get; set; }

    public int? FkProffesorId { get; set; }

    public virtual Class FkClass { get; set; } = null!;

    public virtual Proffesor? FkProffesor { get; set; }

    public virtual Student FkStudent { get; set; } = null!;
}
