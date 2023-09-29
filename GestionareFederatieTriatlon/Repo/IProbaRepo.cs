using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IProbaRepo
    {
        IQueryable<Proba> GetProbeIQueryable();
        void Delete(Proba proba);
        void Create(Proba proba);
        void Update(Proba proba);
    }
}
