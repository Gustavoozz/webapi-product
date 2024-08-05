using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace productsApi.Domains
{
        [Table("Products")]
        public class ProductsDomain
        {
            [Key]
            public Guid IdProduct { get; set; } = Guid.NewGuid();

            [Column(TypeName = "VARCHAR(100)")]
            [Required(ErrorMessage = "O nome do produto é obrigatório!")]
            public string? Nome { get; set; }


            [Column(TypeName = "DECIMAL(18, 2)")]
            [Required(ErrorMessage = "O preço do produto é obrigatório!")]
            public decimal Price { get; set; }
        }
    }

