using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface ILocatieManager
    {
        List<LocatieModel> GetLocatie();
        List<LocatieModelById> GetLocatieInfo(int id);
        List<LocatieModelById> GetLocatieGivenComp(int id);
        void Update(LocatieModel locatieModel);
        void Delete(int id);
        void Create(LocatieModelById model);
    }
}
