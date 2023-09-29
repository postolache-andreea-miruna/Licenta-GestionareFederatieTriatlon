using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface ISportivRepo
    {
        IQueryable<Sportiv> GetSportiviIQueryable();
        IQueryable<Antrenor> GetAntrenoriIQueryable();
        IQueryable<Club> GetCluburiIQueryable();
        void Update(Sportiv sp);
    }
}
