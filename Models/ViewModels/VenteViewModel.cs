using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class VenteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date de vente est requise")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "La date de disponibilité est requise")]
        public DateTime DateDisponibilite { get; set; }

        [Required(ErrorMessage = "Le prix de vente est requis")]
        [Range(0.01, 1000000, ErrorMessage = "Le prix doit être positif")]
        public decimal Prix { get; set; }
    }
}
