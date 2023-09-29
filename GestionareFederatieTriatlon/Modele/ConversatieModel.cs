namespace GestionareFederatieTriatlon.Modele
{
    public class ConversatieModel
    {
        public string codUtilizator { get; set; } //from
        public string codUtilizator2 { get; set; } //to
        public string mesajConversatie { get; set; }
        public DateTime dataTrimitereMesaj { get; set; }
    }
}
