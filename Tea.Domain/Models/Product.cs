using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tea.Domain.Models;


[Table("Products")]
public class Product : BaseModel
{
    [Key]
    //baza danych automatycznie generuje wartość tej kolumny przy INSERT
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName ="varchar(50)")]
    public string Name { get; set; } = string.Empty;


    [Column("Ean", TypeName = "varchar(50)")]
    public string Ean { get; set; } = string.Empty;


    [Column("Price", TypeName = "decimal(5,2)")]
    public decimal Price { get; set; }


    [Column("Stock")]
    public int Stock { get; set; } = 0;


    [Column("Sku", TypeName = "varchar(50)")]
    public string Sku { get; set; } = string.Empty;


    [ForeignKey("CategoryId")]
    //z tego wynika ze category może być pusta
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
