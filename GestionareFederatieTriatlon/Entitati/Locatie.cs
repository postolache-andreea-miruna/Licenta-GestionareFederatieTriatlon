using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Locatie
    {
        [Key]
        public int codLocatie { get; set; }
        public string tara { get; set; }
        public string oras { get; set; }
        public string? strada { get; set; }
        public int? numarStrada { get; set; }

        public string? detaliiSuplimentare { get; set; }

        public ICollection<Competitie>Competitii { get; set; }


    }
}
