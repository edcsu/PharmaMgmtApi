namespace PharmaMgmtApi.ViewModels.Orders;

public class OrderViewModel
{
    public Guid Id { get; set; }

    public string DrugName { get; set; } = string.Empty;

    public string UserFullName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int TotalSum { get; set; }

    public string PaymentType { get; set; } = string.Empty;

    public string CardNumber { get; set; } = string.Empty;
}