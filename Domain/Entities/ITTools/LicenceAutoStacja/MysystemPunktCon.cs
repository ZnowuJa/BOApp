namespace Domain.Entities.ITTools.LicenceAutoStacja;

/// <summary>
/// Tabela definiująca zmiany kontekstu między punktami różnych jednostek
/// </summary>
public partial class MysystemPunktCon
{
    /// <summary>
    /// Klucz główny
    /// </summary>
    public long MysystemPunktConId { get; set; }

    /// <summary>
    /// Punkt logowania źródłowy
    /// </summary>
    public long MysystemPunktInId { get; set; }

    /// <summary>
    /// Punkt logowania docelowy
    /// </summary>
    public long MysystemPunktOutId { get; set; }

    public virtual MysystemPunkt MysystemPunktIn { get; set; } = null!;

    public virtual MysystemPunkt MysystemPunktOut { get; set; } = null!;
}
