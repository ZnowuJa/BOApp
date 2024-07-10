using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WorkFlows;
public class WorkflowTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<WorkflowStep> Steps { get; set; }
}
