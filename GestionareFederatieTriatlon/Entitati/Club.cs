using System.ComponentModel.DataAnnotations;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Club
    {
        [Key]
        public int codClub { get; set; }
        public string nume { get; set; }
        public string email { get; set; }
        public string descriere { get; set; }

        public string? urlPozaClub { get; set; }//nou adaugat

        public ICollection<Utilizator> Utilizatori { get; set; }
        public ICollection<IstoricClub> IstoriceCluburi { get; set; }//si mai nou
    }
}
