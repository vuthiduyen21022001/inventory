namespace HQSOFT.eBiz.Inventor;

public static class InventorDbProperties
{
    public static string DbTablePrefix { get; set; } = "Inventor";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Inventor";
}
