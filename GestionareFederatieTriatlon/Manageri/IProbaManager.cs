using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IProbaManager
    {
        List<ProbaModel?> GetProbe();
        List<ProbaModelById> GetProbaInfo(int id);
        void Delete(int id);
        void Create(ProbaModelById probaModelById);
        void Update(ProbaUpdateModel probaUpdateModel);
        List<ProbaModelComp> GetProbaCompetitie(int id);

        List<int> GetNrParticipProbeComp(int id);
        List<ProbaModelTotal> GetProbeTotal();
    }
}
