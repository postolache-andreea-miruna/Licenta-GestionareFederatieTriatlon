using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IUtilizatorRepo
    {
        IQueryable<Utilizator> GetUtilizatorIQueryable();
    }
}
