using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common;
public class FormFile
{
    public int Id { get; set; }
    public string TmpPath { get; set; }
    public string TmpFileName { get; set; }
}
