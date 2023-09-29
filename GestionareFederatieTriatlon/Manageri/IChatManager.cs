using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IChatManager
    {
        void UpdateCodConexiune(string idUtilizator, string conexiune);
        bool GetDisponibilitate(string idUtilizator);
        void UpdateDisponibilitate(string idUtilizator, bool disponibilitate);
        void CreateMesaj(ConversatieCreateModel model);
        List<MesajAfisare> GetMesaje(string utilizator, string utilizator2);
       // void CreateChat();
        //void CreateConversatie(ConversatieCreateModel model);
        //List<Utilizator> OnlineUser();
        List<string> OnlineUser();
        List<OnlineUser> UsersOnline();
        List<OnlineUser> UsersOffline();

        //string GetNume(string idUtilizator);
        NumeUtilizatorChat GetNume(string idUtilizator);
        //string GetPrenume(string idUtilizator);
        PrenumeUtilizatorChat GetPrenume(string idUtilizator);
        void UpdateCitireMesaj(string idUtilizator, string idUtilizator2, bool citireMesaj);

        List<UtilizatoriOnlineSiMesaje> MessagesUnreadFromUsersByEmail(string emailUser);
        void UpdateCitireMesajSpreUserNelogat(string idUtilizator, string idUtilizator2, bool citireMesaj);
        int NumarMesajeNecitite(string emailUser);

    }
}
