using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class ClubRepo: IClubRepo
    {
        private GestionareFederatieTriatlonContext db;
        public ClubRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Club> GetCluburiIQueryable()
        {
            var cluburi = db.Cluburi;
            return cluburi;
        }
        public void Update(Club club)
        {
            db.Cluburi.Update(club);
            db.SaveChanges();
        }

        public void Delete(Club club)
        {
            db.Cluburi.Remove(club);
            db.SaveChanges();
        }
        public void Create(Club club)
        {
            db.Cluburi.Add(club);
            db.SaveChanges();
        }
    }
}
