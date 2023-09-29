using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IIstoricClubRepo
    {
        void Create(IstoricClub istoricClub);
        IQueryable<IstoricClub> GetIstoricCluburi();
    }
}
