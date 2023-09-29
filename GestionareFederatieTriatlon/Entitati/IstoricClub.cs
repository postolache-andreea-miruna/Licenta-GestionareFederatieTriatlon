namespace GestionareFederatieTriatlon.Entitati
{
    public class IstoricClub
    {
        //codUtilizator, codClub, dataInscriereClub sunt PK compus
        //codUtilizator, codClub sunt si FK
        public string codUtilizator { get; set; }
        public int codClub { get; set; }
        public DateTime dataInscriereClub { get; set; }


        public DateTime dataParasireClub { get; set; }


        public Utilizator Utilizator { get; set; } //are un utilizator
        public Club Club { get; set; } //are un club
    }
}
