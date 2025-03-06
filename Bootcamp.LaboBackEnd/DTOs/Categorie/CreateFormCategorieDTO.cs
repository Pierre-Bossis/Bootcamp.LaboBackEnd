using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Categorie;

public class CreateFormCategorieDTO
{
    [Required, MinLength(2)]
    public string Nom { get; set; }
}
