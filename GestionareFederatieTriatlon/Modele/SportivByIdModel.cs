namespace GestionareFederatieTriatlon.Modele
{
    public class SportivByIdModel
    {
        public int numarLegitimatie { get; set; }
        public string? urlPozaProfil { get; set; }
        public string nume { get; set; }
        public string prenume { get; set; }
        public string gen { get; set; }

        public int anNastere { get; set; }

        public string abonareStiri { get; set; }

        public string numeClub { get; set; }
        public string emailAntrenor { get; set; }
        public string numeAntrenor { get; set; }
        public string prenumeAntrenor { get; set; }
    }
}
