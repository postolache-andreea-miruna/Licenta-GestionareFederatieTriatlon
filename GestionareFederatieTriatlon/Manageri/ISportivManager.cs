using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface ISportivManager
    {
        SportivByIdModel GetSportivById(string id);
        //SportivByIdModelOtherView GetSportivByIdView(string id);
        //List<SportiviAntrenorModel> GetSportiviForAntrenorById(string id);
        List<SportiviAntrenorModel> GetSportiviClubById(int id);
        List<SportiviAntrenorModel> GetSportiviClubByIdSearch(int id, string numeFam, string prenume);
        /*void Update(SportivUpdateModel model);*/
        void Update(SportivEmailUpdateModel model);

        List<SportiviAntrenorModel> GetSportiviForAntrenorByEmail(string emailAntrenor);
        SportivByIdModelOtherView GetSportivByEmailView(string email);
        int GetVarstaSportivLeg(int legitimatie);
        GenSportiv GetGenSportivLeg(int legitimatie);
        EmailSportiv GetEmailSportivLegitimat(int legitimatie);//folosit pentru a-i trimite emailuri
        List<SportiviAntrenorModel> GetSportiviFilterForAntrenorByEmail(string emailAntrenor, string gen = "toate genurile", string anNastere = "toti anii");
    }
}
