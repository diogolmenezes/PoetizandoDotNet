using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Poetizando.Portal.Models
{
    public class Contato
    {
        [Required(ErrorMessage = "Preencha o e-mail")]
        [Display(Name="E-mail:")]
        [Email(ErrorMessage="E-mail inválido")]
        public string Email    { get; set; }

        [Required(ErrorMessage = "Preencha o assunto")]
        [Display(Name = "Assunto:")]
        public string Assunto  { get; set; }

        [Required(ErrorMessage = "Preencha mensagem")]
        [Display(Name = "Mensagem:")]
        public string Mensagem { get; set; }
    }
}
