using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Conversatie
    {
      /*  public int codConversatie { get; set; }
        public string codUtilizator { get; set; }
        public int codChat { get; set; }

        [ForeignKey("Utilizator")]
        public string codUtilizator2 { get; set; }
        public string mesajConversatie { get; set; }
        public DateTime dataTrimitereMesaj { get; set; }
*//*        public DateTime dataPrimireMesaj { get; set; }
        public DateTime dataLivrareMesaj { get; set; }*//*

        public Utilizator Utilizator { get; set; }
        public Chat Chat { get; set; }*/
    }
}
