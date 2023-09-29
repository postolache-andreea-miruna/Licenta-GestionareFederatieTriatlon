using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IAutentificareManager
    {
        Task Inregistrare(InregistrareUtilizatorModel inregistrareUtilizatorModel);
        Task<TokenModel> Logare(LogareUtilizatorModel logareUtilizatorModel);//TokenModel pt ca returneaza un token
        Task<IList<string>> Rol(LogareUtilizatorModel logareUtilizatorModel);
    }
}
