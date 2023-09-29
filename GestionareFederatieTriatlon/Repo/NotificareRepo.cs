using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class NotificareRepo: INotificareRepo
    {
        private GestionareFederatieTriatlonContext db;
        public NotificareRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Notificare> GetNotificare()
        {
            var notificari = db.Notificari;
            return notificari;
        }

        public void Update(Notificare notificare)
        {
            db.Notificari.Update(notificare);
            db.SaveChanges();
        }

        public void Create(Notificare notificare)
        {
            db.Notificari.Add(notificare);
            db.SaveChanges();
        }
        public void Delete(Notificare notificare)
        {
            db.Notificari.Remove(notificare);
            db.SaveChanges();
        }
    }
}
