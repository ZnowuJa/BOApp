using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CoC;
public class InstructionStatus
{
    public int InstructionId {  get; set; }
    public bool IsRead { get; set; } = false;
}
