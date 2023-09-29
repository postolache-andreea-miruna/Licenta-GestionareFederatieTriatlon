using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface ICompetitieRepo
    {
        IQueryable<Competitie> GetCompetitiiIQueryable();
        void Delete(Competitie competitie);
        void Create(Competitie competitie);
        void Update(Competitie competitie);
    }
}
