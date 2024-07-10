using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetLatestAssetNumberByPrefixQuery : IRequest<string>
{
    public string _prefix { get; set; }
    public GetLatestAssetNumberByPrefixQuery(string prefix)
    {
        _prefix = prefix;
    }
}
