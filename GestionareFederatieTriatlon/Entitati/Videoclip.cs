using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Videoclip
    {
        [Key]
        public int codVideo { get; set; }
        public string urlVideo { get; set; }
        public string codYoutubeVideo { get; set; }//nu se va mai folosi

        public Competitie Competitie { get; set; }

        [ForeignKey ("Competitie")]
        public int codCompetitie { get; set; }
    }
}
