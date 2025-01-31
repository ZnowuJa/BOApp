using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting;
public class BillFile
{
    public int Id { get; set; }
    public string FilePath { get; set; }
    public string AttUrl { get; set; }
    public string OriginalFileName { get; set; }

}