using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface ICompetitieManager
    {
        List<CompetitieModel?> GetCompetitii();
        List<CompetitieModelById> GetCompetitieInfo(int id);
        void Delete(int id);
        /*void Delete(CompetitieModelDelete modelDelete);*/
        void Update(CompetitieModelUpdate modelUpdate);
        void Create(CompetitieModelCreate modelCreate);
        List<CompetitieModel> GetCompetitiiSportiv(string email);
        List<CompetitieNume?> GetNumeCompNeanulate();
        List<CompetitieNumeIdModel?> GetNumeIdCompetitii();
        List<CompetitieModelTotal?> GetCompetitiiTotal();
        CompetitieNume GetNumeCompId(int codCompetitie);
    }
}
