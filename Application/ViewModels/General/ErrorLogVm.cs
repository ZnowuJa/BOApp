using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Domain.Entities.Common;

namespace Application.ViewModels.General;
public class ErrorLogVm
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public string? User { get; set; }
    public string? PageUrl { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ErrorLog, ErrorLogVm>().ReverseMap();

    }
}
