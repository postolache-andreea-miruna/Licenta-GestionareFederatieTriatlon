using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Modele
{
    public class ConversatieCreateModel
    {
        public string codUtilizator { get; set; } //from
      //  public int codChat { get; set; }
        public string codUtilizator2 { get; set; } //to
        public string mesajConversatie { get; set; }
        public bool citireMesaj { get; set; }

    }
}
