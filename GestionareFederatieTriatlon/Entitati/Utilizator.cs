using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Utilizator : IdentityUser
    {
        public string nume { get; set; }
        public string prenume { get; set; }
        public bool abonareStiri { get; set; }
        public string? urlPozaProfil { get; set; }

        public bool disponibilitate { get; set; }//nou

        public DateTime dataInscreireClubActual { get; set; }

        /* public int? numarLegitimatie { get; set; }
         public DateTime? dataNastere { get; set; }
         public string? gen { get; set; }*/
        /*public string? gradPregatire { get; set; }*/

        //un utilizator este la un singur club
        [ForeignKey("Club")]
        public int codClub { get; set; }

        public string? codAntrenor { get; set; }

        public string? codConexiune { get; set; } = null;
        public Utilizator AntrenorUtilizator { get; set; }
        public Club Club { get; set; }


        public ICollection<Notificare> Notificari { get; set; }
      //  public ICollection<Conversatie> Conversatii {get; set;} 
        public ICollection<Postare> Postari { get; set; }
        public ICollection<RoluriUtilizator> RoluriUtilizatori { get; set; }

        public ICollection<Mesaj> Mesaje { get; set; }//nou

        public ICollection<IstoricClub> IstoriceCluburi { get; set; }//si mai nou
        public ICollection<ReactiePostare> ReactiiPostari { get; set;}

    }
}
