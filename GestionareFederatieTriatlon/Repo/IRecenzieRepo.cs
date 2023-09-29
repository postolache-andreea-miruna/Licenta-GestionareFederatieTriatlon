using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IRecenzieRepo
    {
        IQueryable<Recenzie> GetRecenzii();
        void Create(Recenzie recenzie);
        void Update(Recenzie recenzie);
        void Delete(Recenzie recenzie);
    }
}
