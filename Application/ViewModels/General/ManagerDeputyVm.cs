using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.General
{
    public class ManagerDeputyVm
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public string LongName { get; set; }

        public List<OrganisationRoleVm>? Deputies { get; set; } = new();
    }
}
