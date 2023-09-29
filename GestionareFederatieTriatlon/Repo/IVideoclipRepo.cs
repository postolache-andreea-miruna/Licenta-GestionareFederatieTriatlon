using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IVideoclipRepo
    {
        IQueryable<Videoclip> GetVideoclipuri();
        void Update(Videoclip videoclip);
        void Delete(Videoclip videoclip);
        void Create(Videoclip videoclip);
    }
}
