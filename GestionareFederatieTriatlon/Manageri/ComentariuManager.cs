using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Manageri
{
    public class ComentariuManager: IComentariuManager
    {
        private readonly IComentariuRepo comRepo;
        private readonly UserManager<Utilizator> utilizatorManager;
        public ComentariuManager(IComentariuRepo comRepo,UserManager<Utilizator> utilizatorManager)
        {
            this.comRepo = comRepo;
            this.utilizatorManager = utilizatorManager;
        }
        public void Update(ComentariuUpdateModel model)
        {
            var comentariu = comRepo.GetComentariiIQueryable()
                .FirstOrDefault(x => x.codComentariu == model.codComentariu);
            if (comentariu == null)
                return;
            comentariu.codComentariu = model.codComentariu;
            comentariu.mesajComentariu = model.mesajComentariu;
            comRepo.Update(comentariu);
        }
        public void Create(ComentariuModelCreate comentariu)
        {
            var utilizatorul = utilizatorManager.FindByEmailAsync(comentariu.emailUtilizatorComentariu);
            var idUtiliz = utilizatorul.Result.Id;

            int maxCodComentariu = 0;
            int nrCodComentarii = comRepo.GetComentariiIQueryable().Count();
            if(nrCodComentarii == 0)
            {
                maxCodComentariu = 0;
            }
            else
            {
                maxCodComentariu = comRepo.GetComentariiIQueryable().Max(c => c.codComentariu);
            }
            var newComentariu = new Comentariu
            {
                codPostare = comentariu.codPostare,
                codUtilizatorComentariu = idUtiliz,
                mesajComentariu = comentariu.mesajComentariu,
                dataComentariu = DateTime.Now,
                codComentariu = maxCodComentariu + 1
            };
            comRepo.Create(newComentariu);
        }

        public void Delete(int idComentariu)
        {
            var comentariu = comRepo.GetComentariiIQueryable()
                .FirstOrDefault(c => c.codComentariu == idComentariu);

            if (comentariu == null)
                return;

            comRepo.Delete(comentariu);
        }
        public List<ComentariuCodMesajModel> GetComentariiCodMesaj()
        {
            var comentarii = comRepo.GetComentariiIQueryable()
                .Select(c => new ComentariuCodMesajModel
                {
                    codComentariu = c.codComentariu,
                    mesajComentariu = c.mesajComentariu
                })
                .OrderBy(c => c.codComentariu)
                .ToList();
            return comentarii;
        }
        public List<ComentariiModel> GetComentariiByIdPostare(int idPostare)
        {
            var utilizator = utilizatorManager.Users;
            var comentarii = comRepo.GetComentariiIQueryable();
            if (comentarii == null)
                return new List<ComentariiModel>();
            var comModel = comentarii
                .Where(c => c.codPostare == idPostare)
                .Select(c => new ComentariiModel
                {
                    mesajComentariu = c.mesajComentariu,
                    dataComentariu = c.dataComentariu,
                    nume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    codComentariu = c.codComentariu,
                    emailUtilizatorComentariu = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.Email).FirstOrDefault()

                })
                .OrderByDescending(c => c.dataComentariu)
                .ToList();

            return comModel;
        }

    }
}
