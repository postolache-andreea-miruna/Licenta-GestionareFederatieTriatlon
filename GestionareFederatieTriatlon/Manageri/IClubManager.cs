using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IClubManager
    {
        List<ClubModel?> GetClub();
        List<ClubModelById> GetClubInfo(int id);
        void Update(ClubUpdateModel clubUpdateModel);
        void Delete(int id);
        void Create(ClubModelById clubCreate);
        List<string> GetNumeCluburi();
        List<ClubModelTotal?> GetCluburi();
    }
}
