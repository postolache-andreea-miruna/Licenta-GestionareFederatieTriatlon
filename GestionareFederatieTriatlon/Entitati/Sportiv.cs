using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Sportiv : Utilizator
    {
        public int numarLegitimatie { get; set; }
        public DateTime dataNastere { get; set; }
        public string gen { get; set; }


        

       

        public ICollection<Recenzie> Recenzii { get; set;}
        public ICollection<Formular> Formulare { get; set; }
        public ICollection<Istoric> Istorice { get; set; }
    }
}
