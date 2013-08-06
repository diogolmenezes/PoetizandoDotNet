using Poetizando.Entidade.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Autor")]
    public partial class Autor : DmgEntidade
    {
        public Autor()
        {
            this.Frases = new List<Frase>();
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Destaque")]
        public bool Destaque { get; set; }

        [Display(Name = "Imagem")]
        [StringLength(200)]
        public string Imagem { get; set; }

        [Display(Name = "Wiki")]
        [StringLength(300)]
        public string Wiki { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Frase> Frases { get; set; }
    }

}
