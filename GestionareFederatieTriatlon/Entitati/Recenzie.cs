using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Recenzie
    {
        [Key]
        public int codRecenzie { get; set; }
        public int numarStele { get; set; }
        public string text { get; set; }

        public DateTime completareRecenzie { get; set; } = DateTime.Now;

        [ForeignKey("Sportiv")]
        public string codUtilizator{ get; set; }
        public Sportiv Sportiv { get; set; }

        [ForeignKey("Competitie")]
        public int codCompetitie { get; set; }
        public Competitie Competitie { get; set; }
    }
}
