using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Produit
{
    public class CreateFormProduitDTO
    {
        [Required(ErrorMessage = "Le champ est requis.")]
        [MinLength(2, ErrorMessage = "Le nom du produit doit posséder au moins 2 caractères.")]
        [RegularExpression(@"^\S.*", ErrorMessage = "Le nom ne doit pas commencer par un ou plusieurs espaces.")]
        public string Nom { get; set; }
        [Required, Range(0,1000000)]
        public decimal Prix { get; set; }
        [Required, Range(0, 1000000)]
        public int Quantite { get; set; }
        [Required, MaxLength(1000)]
        [RegularExpression(@"^\S.*", ErrorMessage = "La description ne doit pas commencer par un ou plusieurs espaces.")]
        public string Description { get; set; }
        [Required]
        public int CategorieId { get; set; }
    }
}
