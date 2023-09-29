using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface ILocatieRepo
    {
        IQueryable<Locatie> GetLocatiiIQueryable();
        IQueryable<Competitie> GetLocatiiCompetitie();
        void Update(Locatie locatie);
        void Delete(Locatie locatie);
        void Create(Locatie locatie);
    }
}
