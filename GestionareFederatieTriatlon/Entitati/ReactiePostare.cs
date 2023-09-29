namespace GestionareFederatieTriatlon.Entitati
{
    public class ReactiePostare
    {
        //codUtilizator si codPostare sunt PK compus, dar si FK
        public string codUtilizator { get; set; }
        public int codPostare { get; set; }
        public bool reactieFericire { get; set; }
        public bool reactieTristete { get; set; }

        public Utilizator Utilizator { get; set; } //are un utilizator
        public Postare Postare { get; set; }
    }
}
