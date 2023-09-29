using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IRecenzieManager
    {
        double GetCompetitieSteleMedie(int id);
        List<RecenziiSportiviModel> RecenziiSportiviCompId(int id);
        void Create(RecenzieCreateModel createModel);
        void Update(RecenzieUpdateModel updateModel);
        void Delete(int id);
        bool RecenzieDataSportivCompId(string email, int idComp);
        List<RecenziileSportivModel> RecenziiSportiv(string email);
        List<RecenzieCodTextModel> GetCodTextRecenzii();
    }
}
