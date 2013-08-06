using Poetizando.Entidade.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Livro")]
    public class Livro : DmgEntidade
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Titulo")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Display(Name = "TituloOriginal")]
        [StringLength(100)]
        public string TituloOriginal { get; set; }

        [Display(Name = "Serie")]
        [StringLength(100)]
        public string Serie { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Autor")]
        public virtual Autor Autor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Editora")]
        public virtual Editora Editora { get; set; }

        [Display(Name = "Paginas")]
        public int Paginas { get; set; }

        [Display(Name = "Url para Compra")]
        [StringLength(300)]
        public string UrlCompra { get; set; }

        [Display(Name = "UrlSkoob")]
        [StringLength(300)]
        public string UrlSkoob { get; set; }

        [Display(Name = "UrlResenha")]
        [StringLength(300)]
        public string UrlResenha { get; set; }
    }    
}
