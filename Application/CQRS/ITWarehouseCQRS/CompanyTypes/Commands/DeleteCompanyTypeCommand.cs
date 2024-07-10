using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class DeleteCompanyTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCompanyTypeCommand(int id)
    {
        Id = id;
    }
}

