using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.Interfaces;
public interface IAssigneeVm 
{
    int Id { get; set; }
    string LongName { get; set; }

    string typeName { get; set; }
}