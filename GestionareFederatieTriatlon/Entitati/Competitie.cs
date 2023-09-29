using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Competitie
    {
        [Key]
        public int codCompetitie { get; set; }
        public string numeCompetitie { get; set; }
        public float taxaParticipare { get; set; }
        public DateTime dataStart { get; set; }
        public DateTime dataFinal { get; set; }
        public string? paginaOficialaCompetitie { get; set; }
        public string statusCompetitie { get; set; } = "activa";

        public ICollection<Recenzie> Recenzii { get; set; }

        [ForeignKey("Tip")]
        public int codTip { get; set; }
        public Tip Tip { get; set; }

        [ForeignKey("Locatie")]
        public int codLocatie { get; set; }
        public Locatie Locatie { get; set; }


        public Videoclip Videoclip { get; set; }
        public ICollection<Istoric> Istorice { get; set; }
    }
}
