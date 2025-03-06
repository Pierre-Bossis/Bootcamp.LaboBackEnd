using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Produit
{
    public class CreateFormProduitDTO
    {
        [Required, MinLength(2)]
        public string Nom { get; set; }
        [Required, Range(0,1000000)]
        public decimal Prix { get; set; }
        [Required, Range(0, 1000000)]
        public int Quantite { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int CategorieId { get; set; }
    }
}
