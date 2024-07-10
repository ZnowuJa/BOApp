using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Companies.Commands;
public class DeleteCompanyCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCompanyCommand(int id)
    {
        Id = id;
    }
}
