using System.ComponentModel.DataAnnotations;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Proba
    {
        [Key]
        public int codProba { get; set; }
        public string numeProba { get; set; }
        public string timpLimita { get; set; } //hh:mm:ss
        public string detaliiDistante { get; set; }

        public ICollection<Istoric> Istorice { get; set; }

    }
}
