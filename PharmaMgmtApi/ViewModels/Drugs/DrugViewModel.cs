namespace PharmaMgmtApi.ViewModels.Drugs;

public class DrugViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public string ImageUrl { get; set; } = String.Empty;

    public int Price { get; set; }

    public int Quantity { get; set; }
}