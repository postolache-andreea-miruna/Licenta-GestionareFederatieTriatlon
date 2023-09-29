using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IIstoricManager
    {
        List<IstoricCreateModel> GetIstoriceForCompetitieId(int codCompetitie);
        List<string> GetNumeProbe(int id);
        List<string> GetCategParticipante(int id);
        List<string> GetCluburiParticipante(int id);
        List<string> GetNumeCompetitie(int id);
        void Create(IstoricCreateModel model);
        void Update(IstoricCreateModel model);
       // List<IstoricSportivBestOfModel> GetBestOfByIdSportiv(string id);
        List<IstoricSportivModel> GetIstoricRezultateByIdSportiv(string id);
        List<IstoricSportivModel> GetIstoricRezultateByLegitimatieSportiv(int numarLegitimatie);
        List<IstoricRezultateCompetitieModel> GetRezultateCompetitie(int id);
        List<IstoricRezultateCompetitieModel> GetRezultateCompetitieNumeProbaCategorieClub(int id, string numeProba = "toate probele", string categorie = "toate categoriile", string club = "toate cluburile");
        List<IerarhiePuncteModel> GetIerarhiePuncteCategClubAnProba(string categ = "toate categoriile", string club = "toate cluburile", string an = "toti anii",
        string proba = "toate probele");

        List<IstoricCluburiTopCompetitieModel> GetTopCluburiByComp(int id);
        List<IstoricCluburiTopCompetitieModel> GetTopCluburiPerAn(string an = "toti anii");

        // List<int> GetStatisticaMedaliiSpAntrComp(string idAntrenor, string numeCompetitie);
        List<int> GetStatisticaMedaliiSpAntrComp(string emailAntrenor, string numeCompetitie);
        // List<double> GetStatisticaPodiumSpAntrComp(string idAntrenor, string numeCompetitie);
        List<double> GetStatisticaPodiumSpAntrComp(string emailAntrenor, string numeCompetitie);
        List<IstoricSportivBestOfModel> GetBestOfByEmailSportiv(string email);
        List<IstoricSportivBestOfModel> GetBestOfByNrLegitimatieSportiv(int numarLegitimatie);
    }
}
