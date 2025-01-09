using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public static class Dictionary
    {
        public static readonly Dictionary<int, decimal> MileageAllowance = new Dictionary<int, decimal>
    {
        { 900, 0.89m },
        { 901, 1.15m }
    };
    }
}


