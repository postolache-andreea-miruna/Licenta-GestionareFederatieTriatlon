using System.ComponentModel.DataAnnotations;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Tip
    {
        [Key]
        public int codTip { get; set; }
        public string tipCompetitie { get; set; }
        public int numarMinimParticipanti { get; set; }

        public ICollection<Competitie> Competitii { get; set; }
    }
}
