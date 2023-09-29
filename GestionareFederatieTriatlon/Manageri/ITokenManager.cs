using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface ITokenManager
    {
        Task<string> CreareToken(Utilizator utilizator);
        bool IsTokenExpired(string token);
    }
}
