using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IAntrenorRepo
    {
        IQueryable<Antrenor> GetAntrenorIQueryable();
        IQueryable<Club> GetClubIQueryable();
        void Update(Antrenor antr);
    }
}
