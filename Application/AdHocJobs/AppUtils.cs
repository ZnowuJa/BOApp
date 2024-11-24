using Application.DTOs;
using AutoMapper;

namespace Application.AdHocJobs;
public static class AppUtils
{
    public static List<AssetMinimal> ConvertAssetDTO2Minimal(List<AssetDTO> assets, IMapper _mapper)
    {
        
        //var minimals = new List<AssetMinimal>();
        var minimals = _mapper.Map<List<AssetMinimal>>(assets);
        //here write a code for convertion.


        return minimals;
    }
}
//public  class Utils
//{
//    public static Expression<Func<T, string>> GetPropertyExpression<T>(Func<T, string> propertyFunc)
//    {
//        return x => propertyFunc(x);
//    }

//    public static void HandleFilterDebounced<T>(FilterColumn<T> column,
//        ChangeEventArgs args,
//        DebounceDispatcher debounceDispatcher,
//        Func<Task> stateHasChangedInvoker)
//    {
//        debounceDispatcher.Debounce(400, _ =>
//        {
//            column.Filter = args.Value.ToString();
//            stateHasChangedInvoker();
//        });
//    }
    