using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class NotificareCreateModel
    {
        public string mesaj { get; set; }
        //public string codUtilizator2 { get; set; } //cel care primeste notificarea
        public string titluNotificare { get; set; }

        public string emailUtilizator { get; set; }
        public int numarLegitimatieUtiliz2 { get; set; }
       // public string codUtilizator { get; set; } //cel care o da

    }
}
