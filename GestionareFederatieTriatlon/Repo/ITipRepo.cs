using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface ITipRepo
    {
        IQueryable<Tip> GetTipIQueryable();
        void Create(Tip tip);
        void Update(Tip tip);
        void Delete(Tip tip);
    }
}
