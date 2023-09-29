using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using Microsoft.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Repo
{
    public class AntrenorRepo: IAntrenorRepo
    {
        private GestionareFederatieTriatlonContext db;
        public AntrenorRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Antrenor> GetAntrenorIQueryable()
        {
            var antrenori = db.Antrenori;
            return antrenori;
        }
        public IQueryable<Club> GetClubIQueryable()
        {
            var cluburi = db.Cluburi;
            return cluburi;
        }
        public void Update(Antrenor antr)
        {
            db.Antrenori.Update(antr);
            db.SaveChanges();
        }
    }
}
