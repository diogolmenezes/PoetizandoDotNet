using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Musica")]
    public class Musica : DmgEntidade
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Autor")]
        public virtual Autor Autor { get; set; }

        [Display(Name = "UrlVideo")]
        [StringLength(300)]
        public string UrlVideo { get; set; }

        [Display(Name = "UrlCifra")]
        [StringLength(300)]
        public string UrlCifra { get; set; }

        [Display(Name = "Letra")]
        public string Letra { get; set; }

        [Display(Name = "Album")]
        [StringLength(50)]
        public string Album { get; set; }
    }    
}
