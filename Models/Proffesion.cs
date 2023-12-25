using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class Proffesion
{
    public int ProffesionId { get; set; }

    public string Title { get; set; } = null!;

    public decimal? MonthlySalary { get; set; }

    public virtual ICollection<Occupation> Occupations { get; set; } = new List<Occupation>();
}
