using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Storage: BaseModel {
    [InverseProperty("Storag")]
    public virtual List<Product>? Products { get; set; } = new();
}