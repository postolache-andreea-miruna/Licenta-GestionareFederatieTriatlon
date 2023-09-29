using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Mesaj
    {
        [Key]
        public int codMesaj { get; set; }

        [ForeignKey("Utilizator")]
        public string codUtilizator { get; set; }//cel care trimite - from
        public string codUtilizator2 { get; set; }//cel care primeste - to
        public string mesajConversatie { get; set; }
        public DateTime dataTrimitereMesaj { get; set; }
        
        public bool citireMesaj { get; set; }

        public Utilizator Utilizator { get; set; }
    }
}
