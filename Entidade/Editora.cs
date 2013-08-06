using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Editora")]
    public class Editora : DmgEntidade
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Display(Name = "Url")]
        [StringLength(300)]
        public string Url { get; set; }
    }
}
