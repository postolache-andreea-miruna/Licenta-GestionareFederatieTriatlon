using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class RecenzieRepo: IRecenzieRepo
    {
        private GestionareFederatieTriatlonContext db;
        public RecenzieRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Recenzie> GetRecenzii()
        {
            var recenzii = db.Recenzii;
            return recenzii;
        }

        public void Create(Recenzie recenzie)
        {
            db.Recenzii.Add(recenzie);
            db.SaveChanges();
        }
        public void Update(Recenzie recenzie)
        {
            db.Recenzii.Update(recenzie);
            db.SaveChanges();
        }
        public void Delete(Recenzie recenzie)
        {
            db.Recenzii.Remove(recenzie);
            db.SaveChanges();
        }

    }
}
