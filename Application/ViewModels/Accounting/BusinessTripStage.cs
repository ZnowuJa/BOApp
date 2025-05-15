using Domain.Entities.Accounting;

namespace Application.ViewModels.Accounting;

public class BusinessTripStage
{
    
    public int Order { get; set; }
    // public bool IsFirstDay { get; set; }
    // public string StageType { get; set; }
    public DateTime TravelFromDateTime { get; set; }
    public DateTime TravelToDateTime { get; set; }
    public Country Country { get; set; }
    
    public bool IsFirstDay { get; set; }
    
    public bool IsAccommodationProvided { get; set; }
    public bool IsTransitUsed { get; set; }
    public bool IsLocalTransportUsed { get; set; }
    public bool IsBreakfastProvided { get; set; }
    public bool IsLunchProvided { get; set; }
    public bool IsDinnerProvided { get; set; }
    
    public int Accomodations { get; set; }
    public int Transits { get; set; }
    public int LocalTransports { get; set; }
    public int Breakfasts { get; set; }
    public int Lunches { get; set; }
    public int Dinners { get; set; }
    
    
}

// public
// StageTypes
// {
//     Travel,
//     LocalTravel,
//     Accomodation,
// }

// public class Travel()
// {
//     
// }

// public class LocalTravel()
// {
//     
// }

// public class Accomodation()
// {
//     public bool CompanyPaid = false;
//     public bool FlatRate = false;
//     public bool Reimburse = false;
//     public string Place;
//     public int Days;
//
// }
