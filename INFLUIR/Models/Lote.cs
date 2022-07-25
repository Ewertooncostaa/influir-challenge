using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFLUIR.Models
{
    [Table("Lote")]
    public class Lote
    {
        [Column("id_lote")]
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Column("codigo_lote")]
        [Display(Name = "Código Lote")]
        [Required]
        public string Codigo { get; set; }

        [Column("id_perimetro")]
        [Display(Name = "Perímetro")]
        [Required]
        public int PerimetroId { get; set; }

        [ForeignKey("PerimetroId")]        
        public virtual Perimetro? Perimetro { get; set; }

        [Column("id_produtor")]
        [Display(Name = "Produtor")]
        [Required]
        public int ProdutorId { get; set; }

        [ForeignKey("ProdutorId")]
        public virtual Produtor? Produtor { get; set; }

    }
}
