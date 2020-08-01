using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastGroupTurmas.ViewModel
{
    public class CursoInsertViewModel
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }

        public int QtdeAluno { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}