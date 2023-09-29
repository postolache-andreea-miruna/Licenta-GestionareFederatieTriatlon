using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class ProbaRepo : IProbaRepo
    {
        private GestionareFederatieTriatlonContext db;
        public ProbaRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Proba> GetProbeIQueryable() 
        {
            var probe = db.Probe;
            return probe;
        }

        public void Delete(Proba proba)
        {
            db.Probe.Remove(proba);
            db.SaveChanges();
        }

        public void Create(Proba proba)
        {
            db.Probe.Add(proba);
            db.SaveChanges();
        }

        public void Update(Proba proba)
        {
            db.Probe.Update(proba);
            db.SaveChanges();
        }
    }
}
