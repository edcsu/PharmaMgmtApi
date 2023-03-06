using System.ComponentModel.DataAnnotations;
using PharmaMgmtApi.Commons;

namespace PharmaMgmtApi.Models;

public class Drug : Auditable
{
    [MaxLength(35)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public int Price { get; set; }

    public int Quantity { get; set; }
}