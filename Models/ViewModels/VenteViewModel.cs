using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class VenteViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Prix { get; set; }
    }
}
