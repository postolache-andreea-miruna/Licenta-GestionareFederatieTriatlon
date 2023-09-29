using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface ITipManager
    {
        List<TipModel?> GetTip();
        List<TipModelById> GetTipInfo(int id);
        void Create(TipModelById model);
        void Update(TipUpdateModel tipUpdateModel);
        void Delete(int id);
        List<TipModelCodNume> GetTipCodNume();
        List<TipModelTotal?> GetTipuri();
    }
}
