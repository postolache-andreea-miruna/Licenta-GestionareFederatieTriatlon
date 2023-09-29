using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Notificare
    {
        [Key]
        public int codNotificare { get; set; }
        public string mesaj { get; set; }

        [ForeignKey("Utilizator2")]
        public string codUtilizator2 { get; set; } //cel care primeste notificarea
        public DateTime dataCreare { get; set; }
        public string titluNotificare { get; set; }
        public bool citireNotificare { get; set; }

        //o notificare - de la un singur utilizator
        [ForeignKey("Utilizator")]
        public string codUtilizator { get; set; } //FK
        public Utilizator Utilizator { get; set; }
        public Utilizator Utilizator2 { get; set; }
    }
}
