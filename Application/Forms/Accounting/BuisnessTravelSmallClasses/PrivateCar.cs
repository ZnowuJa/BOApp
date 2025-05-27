using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses

{
    public class PrivateCar()
    {
        public bool IsSelected { get; set; } = false;
        public string PlateNumber { get; set; } = string.Empty;
        public int EngineSize { get; set; } = 0;
        public int Mileage { get; set; } = 0;
    }
}

