using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Produit;

public class UpdateFormProduitDTO
{
    public int Id { get; set; }
    [Required]
    [RegularExpression(@"^\S.*", ErrorMessage = "Le nom ne doit pas commencer par un ou plusieurs espaces.")]
    public string Nom { get; set; }
    [Required, Range(0,100000)]
    public decimal Prix { get; set; }
    [Required, Range(0,1000)]
    public int Quantite { get; set; }
    [Required, RegularExpression(@"^\S.*", ErrorMessage = "La description ne doit pas commencer par un ou plusieurs espaces.")]
    public string Description { get; set; }
    [Required]
    public int CategorieId { get; set; }
}
