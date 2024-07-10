using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
internal class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
