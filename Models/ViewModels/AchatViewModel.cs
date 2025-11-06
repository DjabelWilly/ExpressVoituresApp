using System.ComponentModel.DataAnnotations;

namespace ExpressVoituresApp.Models
{
    public class AchatViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date est requise")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est requis")]
        [Range(1, 1000000, ErrorMessage = "Le prix doit être positif")]
        public int Prix { get; set; }

        // Validation personnalisée : date d'achat entre 1990 et aujourd'hui
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dateMin = new DateTime(1990, 1, 1);
            var dateMax = DateTime.Today;

            if (Date < dateMin || Date > dateMax)
            {
                yield return new ValidationResult(
                    $"La date doit être comprise entre {dateMin:dd/MM/yyyy} et {dateMax:dd/MM/yyyy}.",
                    new[] { nameof(Date) }
                );
            }
        }

    }
}

