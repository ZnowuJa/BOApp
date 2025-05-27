namespace Domain.Entities.Accounting;

public class Customer
{
    public long KontrahentId { get; set; }

    public string? Nazwa { get; set; }

    public string? Nip { get; set; }

    public bool Przelew { get; set; }
    public string Numer_Fk { get; set; }
    public bool is_Firma { get; set; }
    public long Faktdoc_Id { get; set; }
    public int CC {  get; set; }
}
