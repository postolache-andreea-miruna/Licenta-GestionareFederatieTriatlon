using System.Globalization;

namespace GestionareFederatieTriatlon.Modele
{
    public class AntrenorUpdateModel
    {
        public string emailAntrenor { get; set; }//Id { get; set; }
        public bool abonareStiri { get; set; }
        public string gradPregatire { get; set; }
        public string nume { get; set; }
        public string numeClub { get; set; }
        public string prenume { get; set; }
        public string? urlPozaProfil { get; set; }

    }
}
