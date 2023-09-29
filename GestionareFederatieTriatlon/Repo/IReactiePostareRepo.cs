using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IReactiePostareRepo
    {
        IQueryable<ReactiePostare> GetReactiiPostareIQueryable();
        void Update(ReactiePostare model);
        void Delete(ReactiePostare model);
        void Create(ReactiePostare model);
    }
}
