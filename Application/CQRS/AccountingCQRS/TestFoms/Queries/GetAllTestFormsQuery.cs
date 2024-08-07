using Application.Forms;
using Application.ViewModels;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetAllTestFormsQuery : IRequest<IQueryable<TestFormVm>>
{
}
