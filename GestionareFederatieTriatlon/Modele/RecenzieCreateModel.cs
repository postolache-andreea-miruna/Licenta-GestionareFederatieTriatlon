using GestionareFederatieTriatlon.Entitati;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class RecenzieCreateModel
    {
        public int numarStele { get; set; }
        public string text { get; set; }
        // public DateTime completareRecenzie { get; set; } = DateTime.Now;
        //public string codUtilizator { get; set; }

        public string emailUtilizator { get; set; }
        public int codCompetitie { get; set; }
    }
}
