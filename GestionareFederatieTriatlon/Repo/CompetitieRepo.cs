using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class CompetitieRepo : ICompetitieRepo
    {
        private GestionareFederatieTriatlonContext db;
        public CompetitieRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }
        public IQueryable<Competitie> GetCompetitiiIQueryable()
        {
            var competitii = db.Competitii;
            return competitii;
        }

        public void Delete(Competitie competitie)
        {
            db.Competitii.Remove(competitie);
            db.SaveChanges();
        }

        public void Create(Competitie competitie)
        {
            db.Competitii.Add(competitie);
            db.SaveChanges();
        }

        public void Update(Competitie competitie)
        {
            db.Competitii.Update(competitie);
            db.SaveChanges();
        }
    }
}
