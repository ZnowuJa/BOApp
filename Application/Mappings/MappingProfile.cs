using Application.ViewModels.Accounting;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.Accounting;
using Domain.Entities.ITWarehouse;
using System.Reflection;

namespace Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {

        AppplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

    }

    private void AppplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
                            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo.Invoke(instance, new object[] { this });
        }
    }
}
