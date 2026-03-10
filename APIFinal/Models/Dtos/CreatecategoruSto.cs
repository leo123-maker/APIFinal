using System;
using System.ComponentModel.DataAnnotations;

namespace APIFinal.Models.Dtos;

public class CreatecategoruSto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede tner mas de 50 caracteres")]
    [MinLength(3,ErrorMessage ="El nombre no puede tener menos de 3 caracteres")]
    public string Name {get; set; } = string.Empty;
    
    
}

    
