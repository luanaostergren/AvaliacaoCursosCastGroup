using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CastGroupTurmas.Models
{
    [Table("Curso")]
    public class Curso
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }
        
        public int QtdeAluno { get; set; }
        [Required]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}