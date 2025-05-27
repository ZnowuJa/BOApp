using Application.CQRS.General.ErrorLogs.Commands;
using Application.ViewModels.General;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace BOAppFluentUI.Components.Pages;

public class CustomErrorBoundary : ErrorBoundary
{
    //[Inject]
    //private IEnvironment env { get; set; }
    protected override Task OnErrorAsync(Exception exception)
    {
        


        return base.OnErrorAsync(exception);
              
    }
}
