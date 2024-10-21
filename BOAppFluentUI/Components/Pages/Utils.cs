using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

using Application.Forms;
using Application.Interfaces;
using Application.ITWarehouseCQRS.Employees.Queries;

using Application.ViewModels.General;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BOAppFluentUI.Components.Pages;

public static class Utils
{
    public static Expression<Func<T, string>> GetPropertyExpression<T>(Func<T, string> propertyFunc)
    {
        return x => propertyFunc(x);
    }

    public static void HandleFilterDebounced<T>(FilterColumn<T> column,
        ChangeEventArgs args,
        DebounceDispatcher debounceDispatcher,
        Func<Task> stateHasChangedInvoker)
    {
        debounceDispatcher.Debounce(400, _ =>
        {
            column.Filter = args.Value.ToString();
            stateHasChangedInvoker();
        });
    }
    //public static void HandleDropdownFilter<T>(FilterColumn<T> column, ChangeEventArgs args, Func<Task> stateHasChangedInvoker)
    //{
    //    column.Filter = args.Value.ToString();
    //    stateHasChangedInvoker();
    //}

    //public static void HandleDropdownFilter<T>(FilterColumn<T> column, ChangeEventArgs args, Func<Task> stateHasChangedInvoker)
    //{
    //    var selectedValues = args.Value as List<string>;
    //    if (selectedValues != null)
    //    {
    //        column.SelectedValues = selectedValues;
    //        stateHasChangedInvoker();
    //    }
    //}
    public static void OnSelectedOptionsChanged(IEnumerable<string> selectedOptions, FilterColumn<DeferralPaymentFormVm> column)
    {
        column.SelectedValues = selectedOptions.ToList();
        // Assuming you have a way to trigger StateHasChanged from Utils, otherwise, pass a callback
        // StateHasChanged();
    }
    public static IQueryable<T> ApplyFilters<T>(IQueryable<T> items, List<FilterColumn<T>> filterColumns)
    {
        var query = items;

        foreach (var column in filterColumns)
        {
            if (!string.IsNullOrWhiteSpace(column.Filter))
            {
                query = query.Where(x => column.Property(x).Contains(column.Filter, StringComparison.CurrentCultureIgnoreCase));
            }

            if (column.IsDropdown && column.SelectedValues.Any())
            {
                query = query.Where(x => column.SelectedValues.Contains(column.Property(x)));
            }
        }

        return query.OrderByDescending(r => r.GetType().GetProperty("Number").GetValue(r));
    }

    public static async Task GetUserName(
        AuthenticationStateProvider authenticationStateProvider,
        FormUserContext userContext,
        IMediator mediator)
    {
        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        userContext.User = authenticationState.User;

        var claimType = "name";
        var usernameClaim = userContext.User.Claims.FirstOrDefault(c => c.Type == claimType);
        userContext.LongName = usernameClaim?.Value ?? "unknown";

        claimType = "EnovaEmpId";
        var enovaEmpIdClaim = userContext.User.Claims.FirstOrDefault(c => c.Type == claimType);
        if (enovaEmpIdClaim != null)
        {
            userContext.EnovaEmpId = enovaEmpIdClaim.Value;
            try
            {
                userContext.Employee = await mediator.Send(new GetEmployeeByEnovaIdQuery(int.Parse(userContext.EnovaEmpId)));
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        else
        {
            userContext.EnovaEmpId = "unknown";
        }
    }

    public static bool IsEditDisabled<T>(T context, FormUserContext _userContext) where T : IFormVm
    {
        var roles = _userContext.Employee.Roles.ToList();
        if (_userContext.Employee.Roles.Contains(_userContext.AdminRole))
        {
            _userContext.isFormAdmin = true;
            return false;
        }

        if (context.Status == "AprobataL1")
        {
            var test = context.LVL1_EnovaEmpId == _userContext.EnovaEmpId;
            Console.WriteLine(test);
            return !(context.LVL1_EnovaEmpId == _userContext.EnovaEmpId);
        }
        if (context.Status == "AprobataL2")
        {
            var test = context.Level2Approvers.Any(approver => approver.EmpId.ToString() == _userContext.EnovaEmpId);
            Console.WriteLine(test);
            return !(context.Level2Approvers.Any(approver => approver.EmpId.ToString() == _userContext.EnovaEmpId));

        }
        return true; // Default to disabled if none of the conditions match
    }

    public static void HandleChangeApprover<T>(ChangeEventArgs e, IQueryable<EmployeeVm> itemEmployeesList, T formItem, string approverLevel) where T : IFormVm
    {
        var tempEmp = itemEmployeesList.First(p => p.EnovaEmpId == int.Parse(e.Value.ToString()));
        if (approverLevel == "L1")
        {
            formItem.LVL1_EnovaEmpId = tempEmp.EnovaEmpId.ToString();
            formItem.LVL1_EmployeeName = tempEmp.LongName;
        }
        else if (approverLevel == "L2")
        {
            formItem.LVL2_EnovaEmpId = tempEmp.EnovaEmpId.ToString();
            formItem.LVL2_EmployeeName = tempEmp.LongName;
        }
        //Console.WriteLine(tempEmp.LongName);
    }

    public static string GenerateCsv<T>(IQueryable<T> records)
    {
        var csv = new StringBuilder();
        var properties = typeof(T).GetProperties();

        // Add CSV headers
        csv.AppendLine(string.Join(",", properties.Select(p => p.Name)));

        // Add CSV rows
        foreach (var record in records)
        {
            var values = properties.Select(p => p.GetValue(record, null)?.ToString() ?? string.Empty);
            csv.AppendLine(string.Join(",", values));
        }

        return csv.ToString();
    }

}
