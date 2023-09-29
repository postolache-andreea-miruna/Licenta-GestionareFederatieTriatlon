using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class ComentariuRepo: IComentariuRepo
    {
        private GestionareFederatieTriatlonContext db;
        public ComentariuRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Comentariu> GetComentariiIQueryable()
        {
            var comentarii = db.Comentarii;
            return comentarii;
        }

        public void Delete(Comentariu comentariu)
        {
            db.Comentarii.Remove(comentariu);
            db.SaveChanges();
        }

        public void Create(Comentariu comentariu)
        {
            db.Comentarii.Add(comentariu);
            db.SaveChanges();
        }

        public void Update(Comentariu comentariu)
        {
            db.Comentarii.Update(comentariu);
            db.SaveChanges();
        }
    }
}
