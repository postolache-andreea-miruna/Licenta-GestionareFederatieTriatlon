namespace GestionareFederatieTriatlon.Modele
{
    public class MesajAfisare
    {
        /* public string codUtilizator { get; set; }//cel care trimite - from
         public string codUtilizator2 { get; set; }//cel care primeste - to*/
        public string emailUtilizator { get; set; }//cel care trimite - from
        public string emailUtilizator2 { get; set; }//cel care primeste - to
        public string mesajConversatie { get; set; }
        public DateTime dataTrimitereMesaj { get; set; }
    }
}
