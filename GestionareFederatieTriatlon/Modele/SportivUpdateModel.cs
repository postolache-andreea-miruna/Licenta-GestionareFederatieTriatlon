namespace GestionareFederatieTriatlon.Modele
{
    public class SportivUpdateModel
    {
        public string Id { get; set; }
        public string nume { get; set; }
        public string prenume { get; set; }
        public bool abonareStiri { get; set; }
        public string? urlPozaProfil { get; set; }

        public string numeClub { get; set; }

        public string antrenorNou { get; set; }
    }
}
