using System.ComponentModel.DataAnnotations;
using PharmaMgmtApi.Enums;

namespace PharmaMgmtApi.ViewModels.Orders;

public class OrderCreateViewModel
{
    [Required(ErrorMessage = "Drug Id is required field")]
    public Guid DrugId { get; set; }

    [Required(ErrorMessage = "Quantity is required field")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Payment type is required field")]
    public PaymentType PaymentType { get; set; }

    public string? CardNumber { get; set; }
}