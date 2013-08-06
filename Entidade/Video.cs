using Poetizando.Entidade.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Poetizando.Entidade
{
    [Table("Video")]
    public class Video : DmgEntidade
    {
        public Video()
        {
        }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Texto")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Criação")]        
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Está na fan page")]
        public bool EstaNaFanPage { get; set; }
    }
}
