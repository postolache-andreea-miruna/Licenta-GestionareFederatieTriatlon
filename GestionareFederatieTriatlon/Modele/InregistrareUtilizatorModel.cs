namespace GestionareFederatieTriatlon.Modele
{
    public class InregistrareUtilizatorModel
    {
        public string Email { get; set; }
        public string Parola { get; set; }

        public string CodRol { get; set; }


        public string nume { get; set; }
        public string prenume { get; set; }
        public bool abonareStiri { get; set; }
        public string? urlPozaProfil { get; set; }

        public string? emailAntrenor { get; set; }//din frontend se va trimite email-ul antrenorului care in back va fi convertit in codul antrenorului
        public int codClub { get; set; }

        //sportiv
        public int? numarLegitimatie { get; set; }
        public DateTime? dataNastere { get; set; }
        public string? gen { get; set; }

        //antrenor
        public string? gradPregatire { get; set; }
    }
}
