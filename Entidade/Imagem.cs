using Poetizando.Entidade.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Imagem")]
    public partial class Imagem : DmgEntidade
    {
        public Imagem()
        {
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Frase")]
        public virtual Frase Frase { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Criação")]        
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}
