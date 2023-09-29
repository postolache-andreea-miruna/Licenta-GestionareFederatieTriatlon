namespace GestionareFederatieTriatlon.Modele
{
    public class RecenziiSportiviModel
    {
        public string nume { get; set; }
        public string prenume { get; set; }
        public string? urlPozaProfil { get; set; }

        public int numarStele { get; set; }
        public string text { get; set; }

        public DateTime completareRecenzie { get; set; }
    }
}
