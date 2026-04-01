using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RideEntry
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = "";

        [Required(ErrorMessage = "Zadej název trasy")]
        public string NazevTrasy { get; set; } = "";

        [Required(ErrorMessage = "Zadej vzdálenost")]
        public double Vzdalenost { get; set; }

        [Required(ErrorMessage = "Zadej čas")]
        public int CasHodiny { get; set; }
        public int CasMinuty { get; set; }

        public int Prevyseni { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public string? Poznamka { get; set; }
    }
}