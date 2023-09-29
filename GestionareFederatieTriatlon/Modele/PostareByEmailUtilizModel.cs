namespace GestionareFederatieTriatlon.Modele
{
    public class PostareByEmailUtilizModel
    {
        public string urlPoza { get; set; }
        public int numarReactiiFericire { get; set; }
        public int numarReactiiTristete { get; set; }
        public DateTime dataPostare { get; set; }

        public string descriere { get; set; }


        public string nume { get; set; }
        public string prenume { get; set; }
        public string? urlPozaProfil { get; set; }
        public string email { get; set; }

        public int codPostare { get; set; }
        public List<ComentariiModel> comentarii { get; set; }


    }
}
