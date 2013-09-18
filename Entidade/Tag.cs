using Poetizando.Entidade.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Tag")]
    public class Tag : DmgEntidade
    {
        public Tag()
        {
            Frases = new HashSet<Frase>();
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Tag")]
        [StringLength(50)]
        public string Nome { get; set; }

        public virtual ICollection<Frase> Frases { get; set; }
    }
}
