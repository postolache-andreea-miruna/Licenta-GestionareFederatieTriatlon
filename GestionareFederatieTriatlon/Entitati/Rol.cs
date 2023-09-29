using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Entitati
{
    public class Rol : IdentityRole
    {
        public ICollection<RoluriUtilizator> RoluriUtilizatori { get;set; }
    }
}
