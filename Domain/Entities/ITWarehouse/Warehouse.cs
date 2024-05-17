using BackOfficeApp_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ITWarehouse;

public class Warehouse : SmallAuditableEntity
{
    public int Number { get; set; }
    public string Name { get; set; }

}

