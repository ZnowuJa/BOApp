using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common;
public class ErrorLog
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public string? User { get; set; }
    public string? PageUrl { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
}
