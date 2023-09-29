using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IFormularRepo
    {
        IQueryable<Formular> GetFormularIQueryable();
        void Create(Formular form);
        void Update(Formular form);
        IQueryable<Sportiv> GetSportivi();
    }
}
