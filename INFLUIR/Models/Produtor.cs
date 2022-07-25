using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFLUIR.Models
{
    [Table("Produtor")]
    [Index(nameof(CPF), IsUnique = true)]
    public class Produtor
    {
        [Column("id_produtor")]
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Column("nome_produtor")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("CPF")]
        [Display(Name = "CPF")]       
        public string CPF { get; set; }

    }
}
