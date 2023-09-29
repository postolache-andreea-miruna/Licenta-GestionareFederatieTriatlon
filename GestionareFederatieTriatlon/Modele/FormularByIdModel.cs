using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class FormularByIdModel
    {
        public string pozaProfil { get; set; }
        public string avizMedical { get; set; }
        public string buletin_CertificatNastere { get; set; }
        public string completareFormular { get; set; }
    }
}
