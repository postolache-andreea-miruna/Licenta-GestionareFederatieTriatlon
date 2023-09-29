using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class ReactiePostareRepo : IReactiePostareRepo
    {
        private GestionareFederatieTriatlonContext db;
        public ReactiePostareRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<ReactiePostare> GetReactiiPostareIQueryable()
        {
            var reactii = db.ReactiiPostari;
            return reactii;
        }
        public void Update(ReactiePostare model)
        {
            db.ReactiiPostari.Update(model);
            db.SaveChanges();
        }

        public void Delete(ReactiePostare model)
        {
            db.ReactiiPostari.Remove(model);
            db.SaveChanges();
        }
        public void Create(ReactiePostare model)
        {
            db.ReactiiPostari.Add(model);
            db.SaveChanges();
        }
    }
}
