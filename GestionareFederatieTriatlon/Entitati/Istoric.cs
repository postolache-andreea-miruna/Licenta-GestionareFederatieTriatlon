using System.Globalization;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Istoric
    {
        public string codUtilizator{ get; set; }
        public int codProba { get; set; }
        public int codCompetitie { get; set; }
        public string categorie { get; set; }
        public int locPesteToti { get; set; }

        public int locPerGen { get; set; }
        public int locPerCategorie { get; set; }//locPerCatetogrie
        public string timpTotal { get; set; } //hh:mm:ss
        public string timpInot { get; set; } //hh:mm:ss
        public string timpCiclism { get; set; } //hh:mm:ss
        public string timpAlergare { get; set; } //hh:mm:ss

        public string timpTranzit1 { get; set; } //hh:mm:ss
        public string timpTranzit2 { get; set; } //hh:mm:ss

        public int puncte { get; set; }

        public Sportiv Sportiv { get; set; }
        public Proba Proba { get; set; }
        public Competitie Competitie { get; set; }
    }
}
