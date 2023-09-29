using GestionareFederatieTriatlon.Entitati;
using Microsoft.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Repo
{
    public class LocatieRepo : ILocatieRepo
    {
        private GestionareFederatieTriatlonContext db;
        public LocatieRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Locatie> GetLocatiiIQueryable()
        {
            var locatii = db.Locatii;
            return locatii;
        }
        public IQueryable<Competitie> GetLocatiiCompetitie()
        {
            var locatii = db.Competitii
                .Include(l => l.Locatie);
            return locatii;
        }
        public void Update(Locatie locatie)
        {
            db.Locatii.Update(locatie);
            db.SaveChanges();
        }
        public void Delete(Locatie locatie)
        {
            db.Locatii.Remove(locatie);
            db.SaveChanges();
        }
        public void Create(Locatie locatie)
        {
            db.Locatii.Add(locatie);
            db.SaveChanges();
        }
    }
}
