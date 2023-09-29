using GestionareFederatieTriatlon.Entitati;
using Microsoft.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Repo
{
    public class SportivRepo: ISportivRepo
    {
        private GestionareFederatieTriatlonContext db;
        public SportivRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Sportiv> GetSportiviIQueryable()
        {
            var sportivi = db.Sportivi;
            return sportivi;
        }

        public IQueryable<Antrenor> GetAntrenoriIQueryable()
        {
            var antrenori = db.Antrenori;
            return antrenori;
        }
        public IQueryable<Club> GetCluburiIQueryable()
        {
            var cluburi = db.Cluburi;
            return cluburi;
        }

        public void Update(Sportiv sp)
        {
            db.Sportivi.Update(sp);
            db.SaveChanges();
        }
    }
}
