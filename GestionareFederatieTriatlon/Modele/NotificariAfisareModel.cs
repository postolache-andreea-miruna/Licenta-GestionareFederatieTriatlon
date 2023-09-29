using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class NotificariAfisareModel
    {
        //pentru utilizatorul care a trimis notificarea
        public string nume { get; set; }
        public string prenume { get; set; }
        public string urlPozaProfil { get; set; }
        public DateTime dataCreare { get; set; }
        public string titluNotificare { get; set; }
        public bool citireNotificare { get; set; }
        public int codNotificare { get; set; }
    }
}
