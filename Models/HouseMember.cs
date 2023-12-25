using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class HouseMember
{
    public int Hmid { get; set; }

    public int? FkHouseId { get; set; }

    public int? FkStudentId { get; set; }

    public int? FkProffesorId { get; set; }
}
