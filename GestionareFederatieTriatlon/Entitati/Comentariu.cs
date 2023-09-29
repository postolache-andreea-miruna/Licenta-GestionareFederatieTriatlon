namespace GestionareFederatieTriatlon.Entitati
{
    public class Comentariu
    {
        public int codPostare { get; set; }
        public int codComentariu { get; set; }
        public string mesajComentariu { get; set; }
        public string codUtilizatorComentariu { get; set; }
        public DateTime dataComentariu { get; set; } = DateTime.Now;//nou
        public Postare Postare { get; set; }
    }
}
