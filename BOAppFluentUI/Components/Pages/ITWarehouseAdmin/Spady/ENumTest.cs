using System.Runtime.Serialization;

namespace BOAppFluentUI.Components.Pages.ITWarehouseAdmin.Spady;

public class ENumTest
{
    public ENumTest()
    {
        TaskItemPriorityEnum x = new TaskItemPriorityEnum();
        x = TaskItemPriorityEnum.Low;
    }
}
public enum TaskItemPriorityEnum
{
    [EnumMember(Value = "Low")]
    Low,
    [EnumMember(Value = "Normal")]
    Normal,
    [EnumMember(Value = "High")]
    High,
}

