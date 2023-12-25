using System;
using System.Collections.Generic;

namespace IDatabasProjektSQLEF.Models;

public partial class House
{
    public int HousesId { get; set; }

    public string? HouseName { get; set; }

    public string? HouseAttributes { get; set; }

    public string? HouseAnimal { get; set; }

    public string? HouseGhost { get; set; }

    public string? HouseCommonRoom { get; set; }

    public string? HouseHead { get; set; }

    public string? HouseFounder { get; set; }
}
