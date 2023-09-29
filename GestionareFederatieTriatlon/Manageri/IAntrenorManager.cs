using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IAntrenorManager
    {
        List<AntrenoriModel> GetAntrenori();
        AntrenorByIdModel GetAntrenorInfo(string emailAntrenor);
        AntrenorByIdViewSpModel GetAntrenorInfoBySp(string id);
        List<AntrenoriClubModel> GetAntrenoriByClubId(int id);
        void Update(AntrenorUpdateModel model);
        int GetNumarSportiviActualiAntr(string email);
        List<AntrenoriClubMailModel> GetAntrenoriSearchByClubId(int id, string numeFam, string prenume);
       // List<AntrenoriClubModel> GetAntrenoriSearchByClubId(int id, string numeFam, string prenume);
    }
}
