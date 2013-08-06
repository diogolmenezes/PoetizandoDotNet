using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Estatistica")]
    public class Estatistica : DmgEntidade
    {
        public Estatistica()
        {
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Funcionalidade")]
        public string Funcionalidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Acessos")]
        public int Acessos { get; set; }
    }
}
