using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IPostareRepo
    {
        IQueryable<Postare> GetPostari();
        void Delete(Postare postare);
        void Update(Postare postare);
        void Create(Postare postare);
    }
}
