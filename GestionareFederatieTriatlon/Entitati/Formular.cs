using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Formular
    {
        [Key]
        public int codFormular { get; set; }
        public string pozaProfil { get; set; }
        public string avizMedical { get; set; }
        public string buletin_CertificatNastere { get; set; }
        public DateTime completareFormular { get; set; } = DateTime.Now;

        [ForeignKey("Sportiv")]
        public string codUtilizator { get; set; }
        public Sportiv Sportiv { get; set; }
    }
}
