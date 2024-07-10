using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Common;
public class Organisation : SmallAuditableEntity
{
    public string Name { get; set; }
    public string Make {  get; set; }
    public string Description { get; set; }
    public string SapNumber { get; set; }
    public string Role_SalesManager { get; set; } 
    public string Role_ServiceManager { get; set; } 
    public string Role_DealerDirector { get; set; } 
    public string Role_RegionDirector { get; set; } 
    public string Role_SettlementsTeam { get; set; }


}
