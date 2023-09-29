using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Postare
    {
        [Key]
        public int codPostare { get; set; }
        public string urlPoza { get; set; }
        public int numarReactiiFericire { get; set; }
        public int numarReactiiTristete { get; set; }
        public DateTime dataPostare { get; set; } = DateTime.Now;

        public string? descriere { get; set; }

        //o postare e publicata de un singur utilizator
        [ForeignKey("Utilizator")]
        public string codUtilizator { get; set; }
        public Utilizator Utilizator { get; set; }

        //contine mai multe comentarii
        public ICollection<Comentariu> Comentarii { get; set; }
        public ICollection<ReactiePostare> ReactiiPostari { get; set; }
    }
}
