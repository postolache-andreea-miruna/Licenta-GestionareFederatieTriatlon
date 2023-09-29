using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class PostareRepo: IPostareRepo
    {
        private GestionareFederatieTriatlonContext db;
        public PostareRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Postare> GetPostari()
        {
            var postari = db.Postari;
            return postari;
        }

        public void Delete(Postare postare)
        {
            db.Postari.Remove(postare);
            db.SaveChanges();
        }

        public void Update(Postare postare)
        {
            db.Postari.Update(postare);
            db.SaveChanges();
        }

        public void Create(Postare postare)
        {
            db.Postari.Add(postare);
            db.SaveChanges();
        }
    }
}
