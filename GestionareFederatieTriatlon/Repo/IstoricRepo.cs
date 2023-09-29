using GestionareFederatieTriatlon.Entitati;
using Microsoft.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Repo
{
    public class IstoricRepo: IIstoricRepo
    {
        private GestionareFederatieTriatlonContext db;
        public IstoricRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Istoric> GetIstoricProbaCompetitie()
        {
            var istoric = db.Istorice
                .Include(p => p.Proba)
                .Include(c => c.Competitie);
            return istoric;
        }

        public IQueryable<Sportiv> GetSportivi()
        {
            var sportiv = db.Sportivi;
            return sportiv;
        }

        public IQueryable<Proba> GetProbe()
        {
            var proba = db.Probe;
            return proba;
        }

        public IQueryable<Competitie> GetCompetitii()
        {
            var comp = db.Competitii;
            return comp;
        }

        public IQueryable<Istoric> GetIstoricProbaCompetitieSportiv() // la competitia X (la pagina cu competitii)
        {
            var istoric = GetIstoricProbaCompetitie()
                .Include(s => s.Sportiv);
            return istoric;
        }

        public void Create(Istoric istoric)
        {
            db.Istorice.Add(istoric);
            db.SaveChanges();
        }
        public void Update(Istoric istoric)
        {
            db.Istorice.Update(istoric);
            db.SaveChanges();
        }
    }
}
