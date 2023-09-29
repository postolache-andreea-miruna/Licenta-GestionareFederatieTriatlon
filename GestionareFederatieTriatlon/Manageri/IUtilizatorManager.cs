using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IUtilizatorManager
    {
        PozaUtilizator GetPozaUtilizator(string email);
        bool GetAbonare(string email);
        List<DetaliiTrimitereEmail> GetDetaliiTrimitereEmail();
    }
}
