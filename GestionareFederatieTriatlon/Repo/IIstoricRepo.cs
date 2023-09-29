using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IIstoricRepo
    {
        IQueryable<Istoric> GetIstoricProbaCompetitie();
        IQueryable<Istoric> GetIstoricProbaCompetitieSportiv();
        void Create(Istoric istoric);
        void Update(Istoric istoric);
        IQueryable<Sportiv> GetSportivi();
        IQueryable<Proba> GetProbe();
        IQueryable<Competitie> GetCompetitii();
    }
}
