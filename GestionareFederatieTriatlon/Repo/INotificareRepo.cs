using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface INotificareRepo
    {
        IQueryable<Notificare> GetNotificare();
        void Create(Notificare notificare);
        void Update(Notificare notificare);
        void Delete(Notificare notificare);
    }
}
