using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Occupation
{
    public int Occid { get; set; }

    public int? FkProffesorId { get; set; }

    public int? FkProffesionId { get; set; }

    public virtual Proffesion? FkProffesion { get; set; }

    public virtual Proffesor? FkProffesor { get; set; }
}
