using System.ComponentModel.DataAnnotations;
using PharmaMgmtApi.Commons;
using PharmaMgmtApi.Enums;

namespace PharmaMgmtApi.Models;

public class Order : Auditable
{
    public Guid DrugId { get; set; }

    public virtual Drug Drug { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int Quantity { get; set; }

    public PaymentType PaymentType { get; set; }

    [MaxLength(19)]
    public string? CardNumber { get; set; }
}