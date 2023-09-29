using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;

namespace GestionareFederatieTriatlon.Manageri
{
    public class UtilizatorManager: IUtilizatorManager
    {
        private readonly IUtilizatorRepo repo;
        public UtilizatorManager(IUtilizatorRepo repo)
        {
            this.repo = repo;
        }
        public List<DetaliiTrimitereEmail> GetDetaliiTrimitereEmail()
        {
            var antrenori = repo.GetUtilizatorIQueryable()
                .Where(u => u.codAntrenor ==null)
                .Select(u=> new DetaliiTrimitereEmail
                {
                    nume = u.nume,
                    prenume= u.prenume,
                    email = u.Email,
                    abonareStiri = u.abonareStiri
                })
                .ToList();
            return antrenori;
        }
        public PozaUtilizator GetPozaUtilizator(string email)
        {
            var utilizatorPoza = repo.GetUtilizatorIQueryable()
               .Where(a => a.Email == email)
               .Select(a => new PozaUtilizator
               {
                   urlPozaProfil = a.urlPozaProfil
               })
               .FirstOrDefault();
           
           return utilizatorPoza;
        }

        public bool GetAbonare(string email)
        {
            var abonare = repo.GetUtilizatorIQueryable()
               .Where(a => a.Email == email)
               .Select(a =>  a.abonareStiri)
               .FirstOrDefault();

            return abonare;
        }
    }
}
