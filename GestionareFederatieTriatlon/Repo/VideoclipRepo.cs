using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class VideoclipRepo: IVideoclipRepo
    {
        private GestionareFederatieTriatlonContext db;
        public VideoclipRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Videoclip> GetVideoclipuri()
        {
            var video = db.Videoclipuri;
            return video;
        }

        public void Update(Videoclip videoclip)
        {
            db.Videoclipuri.Update(videoclip);
            db.SaveChanges();
        }

        public void Delete(Videoclip videoclip)
        {
            db.Videoclipuri.Remove(videoclip);
            db.SaveChanges();
        }

        public void Create(Videoclip videoclip)
        {
            db.Videoclipuri.Add(videoclip);
            db.SaveChanges();
        }

    }
}
