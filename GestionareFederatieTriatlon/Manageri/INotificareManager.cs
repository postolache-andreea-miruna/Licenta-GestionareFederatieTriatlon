using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface INotificareManager
    {
        // List<NotificariAfisareModel> GetNotificariIdUtilizLogat(string idUtilizator);
        List<NotificariAfisareModel> GetNotificariIdUtilizLogat(string emailUtilizator);
        NotificareById GetNotificareById(int id);
        void Create(NotificareCreateModel notifCreate);
        void NotificareCititaUpdate(int id);
        int GetNrNotificariNecititeIdUtilizLogat(string emailUtilizator);
        void Delete(int id);
        List<NotificareCodMesajModel> GetCodMesajNotificari();
        List<ListaGet> GetLista(string emailUtilizator, int numarLegitimatieUtiliz2);
    }
}
