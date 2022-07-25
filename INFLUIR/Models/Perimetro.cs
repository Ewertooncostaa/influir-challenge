using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFLUIR.Models
{
    [Table("Perimetro")]
    public class Perimetro
    {
        [Column("id_perimetro")]
        [Display(Name = "Código")]
        [Key]
        public int Id { get; set; }

        [Column("nome_perimetro")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
