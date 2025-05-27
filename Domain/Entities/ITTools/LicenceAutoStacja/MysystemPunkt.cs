namespace Domain.Entities.ITTools.LicenceAutoStacja;

public partial class MysystemPunkt
{
    public long MysystemPunktId { get; set; }

    public long? JednostkaOrgId { get; set; }

    public string Nazwa { get; set; } = null!;

    /// <summary>
    /// Numer kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk
    /// </summary>
    public string? PunktTelefon { get; set; }

    /// <summary>
    /// Email kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk
    /// </summary>
    public string? PunktEmail { get; set; }
    
    public virtual ICollection<MysystemPunktCon> MysystemPunktConMysystemPunktIns { get; set; } = new List<MysystemPunktCon>();

    public virtual ICollection<MysystemPunktCon> MysystemPunktConMysystemPunktOuts { get; set; } = new List<MysystemPunktCon>();
}
