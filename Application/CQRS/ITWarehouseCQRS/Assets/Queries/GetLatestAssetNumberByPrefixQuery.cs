using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetLatestAssetNumberByPrefixQuery : IRequest<string>
{
    public string _prefix { get; set; }
    public GetLatestAssetNumberByPrefixQuery(string prefix)
    {
        _prefix = prefix;
    }
}
