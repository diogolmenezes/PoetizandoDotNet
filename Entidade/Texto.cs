using Poetizando.Entidade.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poetizando.Entidade
{
    [Table("Texto")]
    public class Texto : DmgEntidade
    {
        public Texto()
        {
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Texto")]
        public string Post { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Imagem")]
        public string Imagem { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Criação")]        
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Está na fan page")]
        public bool EstaNaFanPage { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Autor")]
        public virtual Autor Autor { get; set; }
    }
}
