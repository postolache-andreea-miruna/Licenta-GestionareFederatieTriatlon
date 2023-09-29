using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IClubRepo
    {
        IQueryable<Club> GetCluburiIQueryable();
        void Update(Club club);
        void Delete(Club club);
        void Create(Club club);
    }
}
