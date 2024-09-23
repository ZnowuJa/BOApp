using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BackOfficeApp_Domain.Common;

namespace Domain.Entities.CoC;
public class Position : SmallAuditableEntity
{

    public string Cat {  get; set; }
    public string Name { get; set; }
    public int? GroupCoCId { get; set; }
    public GroupCoC? GroupCoC { get; set; }

}
