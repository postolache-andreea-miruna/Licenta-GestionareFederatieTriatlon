using GestionareFederatieTriatlon.Entitati;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class CompetitieModelById
    {
        public string numeCompetitie { get; set; }
        public float taxaParticipare { get; set; }
        public DateTime dataStart { get; set; }
        public DateTime dataFinal { get; set; }
        public string? paginaOficialaCompetitie { get; set; }

        public string tipCompetitie { get; set; }
        public int numarMinimParticipanti { get; set; }

    }
}
