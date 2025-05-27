using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;

using Application.CQRS.General.FormFiles.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;

using Application.ViewModels.General;

using AutoMapper;
using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.FluentUI.AspNetCore.Components;
using Newtonsoft.Json;

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

    public static void OnSelectedOptionsChanged<T>(IEnumerable<string> selectedOptions, FilterColumn<T> column)
    {
        column.SelectedValues = selectedOptions.ToList();
        // Assuming you have a way to trigger StateHasChanged from Utils, otherwise, pass a callback
        // StateHasChanged();
    }
    public static IQueryable<T> ApplyFilters<T>(IQueryable<T> items, List<FilterColumn<T>> filterColumns)
    {
        if (items == null)
        {
            return Enumerable.Empty<T>().AsQueryable();
        }
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

        return query.OrderByDescending(r => r.GetType().GetProperty("Id").GetValue(r));
    }

    public static async Task GetUserRoles(FormUserContext userContext)
    {
        if (userContext.User.Identity.IsAuthenticated)
        {
            var userRoles = userContext.User.Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .Distinct()
                .ToList();
            userContext.Employee.Roles = userRoles;
        }
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
                var empId = int.Parse(userContext.EnovaEmpId);
                if (userContext != null)
                    userContext.Employee = await mediator.Send(new GetEmployeeByEnovaIdQuery(empId));
                //await GetUserRoles(userContext);
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

        if (userContext.Employee.Roles.Contains(userContext.AdminRole) || userContext.Employee.Roles.Contains(userContext.ITRole))
        {
            userContext.isFormAdmin = true;
        }
        else
        {
            userContext.isFormAdmin = false;
        }

    }
    public static bool IsEditDisabled<T>(T context, FormUserContext _userContext) where T : IFormVm
    {
        var roles = _userContext.Employee.Roles.ToList();
        string typeName = typeof(T).Name;
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

        return true;
    }

    public static bool IsAccEditDisabled<T>(T context, FormUserContext _userContext) where T : IFormAccounting
    {
        var roles = _userContext.Employee.Roles.ToList();
        string typeName = typeof(T).Name;
        if (_userContext.isFormAdmin)
        {
            return false;
        }
        if (_userContext.Employee.Roles.Contains(_userContext.AdminRole))
        {
            _userContext.isFormAdmin = true;
            return false;
        }

        if ((context.Status == "Rejestracja") || (context.Status == "Rozliczenie") || (context.Status == "Odrzucone"))
        {
            var test = context.EnovaEmpId == _userContext.EnovaEmpId.ToString();
            Console.WriteLine(test);
            return !(context.EnovaEmpId == _userContext.EnovaEmpId.ToString());
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



        return true;
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
    public static byte[] GenerateCsvPL<T>(IQueryable<T> records)
    {
        var csv = new StringBuilder();
        var properties = typeof(T).GetProperties();

        // Add CSV headers
        csv.AppendLine(string.Join("|", properties.Select(p => p.Name)));

        // Add CSV rows
        foreach (var record in records)
        {
            var values = properties.Select(p => p.GetValue(record, null)?.ToString() ?? string.Empty);
            csv.AppendLine(string.Join("|", values));
        }
        //Console.WriteLine(csv.ToString());
        // Convert the CSV content to a byte array using UTF-8 encoding
        //return Encoding.UTF8.GetBytes(csv.ToString());
        // Convert the CSV content to a byte array using UTF-8 encoding
        var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());

        // Add BOM to the beginning of the byte array
        var bom = new byte[] { 0xEF, 0xBB, 0xBF };
        var csvWithBom = bom.Concat(csvBytes).ToArray();

        return csvWithBom;
    }
    public static async Task OnFileUploadedAsync<TContent>(FluentInputFileEventArgs file, IFileService fileService, IMediator mediator, string sessionId, TContent content, List<FormFileVm> addedFiles, ILogger logger)
        where TContent : IFormVm
    {
        var fileInfo = new Dictionary<string, string>();
        try
        {
            fileInfo = await fileService.UploadTemporaryFileAsync(file.Stream, file.Name, sessionId);
        }
        catch (WebException ex)
        {
            logger.LogError(ex.Message);
        }

        if (fileInfo.Any())
        {
            logger.LogInformation(fileInfo.ToString());
        }

        // fileInfo = await fileService.UploadTemporaryFileAsync(file.Stream, file.Title, sessionId);
        var resultFile = new FormFileVm
        {
            TmpPath = fileInfo["TmpPath"],
            TmpFileName = fileInfo["TmpFileName"],
            TmpFileExtension = fileInfo["TmpFileExtension"],
            OriginalFileName = fileInfo["OriginalFileName"],
            Prefix = content.NumberPrefix,
            FolderName = content.FolderName,
            FormClassName = typeof(TContent).Name,
            FormId = content.Id,
        };
        var fileId = await mediator.Send(new CreateFormFileCommand(resultFile));
        resultFile.Id = fileId;
        logger.LogInformation($"File {fileId} uploaded");
        content.FormFiles.Add(resultFile);
        addedFiles.Add(resultFile);
    }

    public static async Task DeleteFile<TContent>(int fileId, IQueryable<FormFileVm> formFiles, IFileService fileService, IMediator mediator, List<FormFileVm> addedFiles, TContent content)
        where TContent : IFormVm
    {
        var fileToDelete = formFiles.FirstOrDefault(f => f.Id == fileId);

        await mediator.Send(new DeleteFormFileCommand(fileId));
        fileService.DeleteFile(fileToDelete.TmpPath);
        fileService.DeleteFile(fileToDelete.DstPath);

        formFiles = formFiles.Where(file => file.Id != fileId).AsQueryable();
        addedFiles.Remove(fileToDelete);
        content.FormFiles = formFiles.ToList();
    }
}

public static class QueryableExtensions
{
    public static IQueryable<TDestination> ProjectTo<TSource, TDestination>(this IQueryable<TSource> source, IMapper mapper)
    {
        return source.Select(item => mapper.Map<TDestination>(item));
    }
}