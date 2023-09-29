using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Entitati
{
    public class RoluriUtilizator : IdentityUserRole<string>
    {
        public virtual Utilizator Utilizator { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
