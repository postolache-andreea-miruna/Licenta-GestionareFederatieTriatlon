using GestionareFederatieTriatlon.Entitati;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class CompetitieModelUpdate
    {
        public int codCompetitie { get; set; }
        public string numeCompetitie { get; set; }
        public float taxaParticipare { get; set; }
        public DateTime dataStart { get; set; }
        public DateTime dataFinal { get; set; }
        public string? paginaOficialaCompetitie { get; set; }
        public string statusCompetitie { get; set; }

        public int codLocatie { get; set; }
    }
}
