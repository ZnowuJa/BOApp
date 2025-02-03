using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.AdHocJobs;
using Application.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Components.Forms;

using Quartz;

namespace Application.BackgroundJobs;
public class AddCoCOnboardingTimeSpan : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    private int? generated { get; set; }
    public AddCoCOnboardingTimeSpan(IAppDbContext appDbContext, IMediator mediator)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        MarkEmployeesGroupJob job1 = new MarkEmployeesGroupJob(_mediator);
        job1.Execute();

        var today = DateTime.Now;
        AddCoCOnboardingsAdHocJob job2 = new AddCoCOnboardingsAdHocJob(_mediator, today, today);

        generated = await job2.Execute();

        return;
    }
}

