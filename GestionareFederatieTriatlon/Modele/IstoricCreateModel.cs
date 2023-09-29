namespace GestionareFederatieTriatlon.Modele
{
    public class IstoricCreateModel
    {
        public int numarLegitimatie { get; set; }//in loc codUtilizator
        public string numeProba { get; set; }//int codProba
        public string numeCompetitie { get; set; }//int codComp
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
    }
}
