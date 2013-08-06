using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("TagFrase")]
    public class TagFrase : DmgEntidade
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Frase")]
        public virtual Frase Frase { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Tag")]
        public virtual Tag Tag { get; set; }
    }
}
