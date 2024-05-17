using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class UpdateUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public UpdateUnitCommand(int id, string name, string shortName)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
    }
}
