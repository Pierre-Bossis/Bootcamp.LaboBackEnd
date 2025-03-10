using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Categorie;

public class CreateFormCategorieDTO
{
    [Required(ErrorMessage = "Le champ est requis.")]
    [MinLength(2, ErrorMessage = "La catégorie doit posséder au moins 2 caractères.")]
    [RegularExpression(@"^\S.*", ErrorMessage = "Le nom ne doit pas commencer par un ou plusieurs espaces.")]
    public string Nom { get; set; }
}
