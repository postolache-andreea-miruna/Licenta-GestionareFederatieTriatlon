using System.Globalization;

namespace GestionareFederatieTriatlon.Modele
{
    public class CompetitieModel
    {
        public int codCompetitie { get; set; }
        public string statusCompetitie { get; set; }
        public string numeCompetitie { get; set; }
        public DateTime dataStart { get; set; }
        public DateTime dataFinal { get; set; }

        public string tipCompetitie { get; set; }
    }
}
