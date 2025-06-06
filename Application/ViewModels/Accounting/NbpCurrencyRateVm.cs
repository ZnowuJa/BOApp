﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public record NbpCurrencyRateVm(
    int Id,            // This represents the identity column
    string Currency,   // This represents the Currency column
    string Code,       // This represents the Code column
    decimal Mid,       // This represents the Mid column
    DateOnly RateDate  // This represents the Rate_date column
);

}

