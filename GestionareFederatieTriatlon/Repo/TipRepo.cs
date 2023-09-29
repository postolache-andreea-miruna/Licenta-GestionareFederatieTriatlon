using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class TipRepo: ITipRepo
    {
        private GestionareFederatieTriatlonContext db;
        public TipRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Tip> GetTipIQueryable()
        {
            var tipuri = db.Tipuri;
            return tipuri;
        }

        public void Create(Tip tip)
        {
            db.Tipuri.Add(tip);
            db.SaveChanges();
        }
        public void Update(Tip tip)
        {
            db.Tipuri.Update(tip);
            db.SaveChanges();
        }
        public void Delete(Tip tip)
        {
            db.Tipuri.Remove(tip);
            db.SaveChanges();
        }
    }
}
