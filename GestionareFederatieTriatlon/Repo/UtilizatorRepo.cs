using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class UtilizatorRepo: IUtilizatorRepo
    {
        private GestionareFederatieTriatlonContext db;
        public UtilizatorRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Utilizator> GetUtilizatorIQueryable()
        {
            var utilizatori = db.Utilizatori;
            return utilizatori;
        }
    }
}
