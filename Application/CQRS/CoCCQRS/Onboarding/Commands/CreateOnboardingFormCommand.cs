using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms;
using MediatR;

namespace Application.CQRS.CoCCQRS.Onboarding.Commands;
public class CreateOnboardingFormCommand : IRequest<OnboardingFormVm>
{
    public OnboardingFormVm Item { get; set; }

    public CreateOnboardingFormCommand(OnboardingFormVm item)
    {
        Item = item;
    }
}
