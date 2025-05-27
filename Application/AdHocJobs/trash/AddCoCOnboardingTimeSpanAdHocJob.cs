using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.BackgroundJobs;
using Application.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;

using Quartz;


//
//
// THIS CLASS runs Mark Employees and Add Onboardings AdHocJobs
//



namespace Application.AdHocJobs;
public class AddCoCOnboardingTimeSpanAdHocJob 

{

    private readonly IMediator _mediator;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;
    public DateTime From { get; }
    public DateTime To { get; }

    

    public AddCoCOnboardingTimeSpanAdHocJob(IMediator mediator, DateTime from, DateTime to, IEmailService mailService, IConfiguration configuration)
    {
        _mediator = mediator;
        From = from;
        To = to;
        _mailService = mailService;
        _configuration = configuration;
    }


    public async Task<int> Execute()
    {
        int generated = 0;

        //MarkEmpCoCGroupByJobCodeAdHocJob job1 = new MarkEmpCoCGroupByJobCodeAdHocJob(_mediator);
        //await job1.Execute();


        AddCoCOnboardingsAdHocJob job2 = new AddCoCOnboardingsAdHocJob(_mediator, From, To, _mailService, _configuration);

        generated = await job2.Execute();

        return generated;
    }
    
}

