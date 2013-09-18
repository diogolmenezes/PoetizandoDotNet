using Poetizando.Entidade.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Poetizando.Entidade
{
    [Table("Frase")]    
    public partial class Frase : DmgEntidade
    {
        public Frase()
        {
            Tags = new HashSet<Tag>();
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Frase")]
        public string Texto { get; set; }        

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Criação")]        
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Autor")]
        public virtual Autor Autor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Oficial")]
        public bool Oficial { get; set; }

        [Display(Name = "Livro")]
        public virtual Livro Livro { get; set; }

        [Display(Name = "Musica")]
        public virtual Musica Musica { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Está na fan page")]
        public bool EstaNaFanPage { get; set; }

        public bool PossuiLivro { get { return Livro != null; } }

        public bool PossuiMusica { get { return Musica != null; } }

        public virtual ICollection<Tag> Tags { get; set; }

  
    }
}
