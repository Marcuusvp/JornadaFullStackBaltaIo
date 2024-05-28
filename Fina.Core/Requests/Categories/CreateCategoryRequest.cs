using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "I Titulo deve conter no máximo 80 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida")]
        public string Description{ get; set; } = string.Empty;
    }
}