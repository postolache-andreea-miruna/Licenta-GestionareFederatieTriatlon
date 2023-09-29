using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IEmailServ
    {
        bool TrimiteEmail(DateEmail detalii);
    }
}
