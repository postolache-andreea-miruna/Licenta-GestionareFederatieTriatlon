using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IComentariuRepo
    {
        IQueryable<Comentariu> GetComentariiIQueryable();
        void Delete(Comentariu comentariu);
        void Create(Comentariu comentariu);
        void Update(Comentariu comentariu);
    }
}
