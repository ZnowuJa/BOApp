using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoC;
public class GroupCoC
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public ICollection<Position> Positions { get; set; }
    public ICollection<InstructionCoC> Instructions { get; set; }

}
