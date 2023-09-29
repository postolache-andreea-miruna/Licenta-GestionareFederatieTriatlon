using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;

namespace GestionareFederatieTriatlon.Manageri
{
    public class ProbaManager: IProbaManager
    {
        private readonly IProbaRepo probaRepo;
        private readonly IIstoricRepo istoricRepo;

        public ProbaManager(IProbaRepo probaRepo, IIstoricRepo istoricRepo)
        {
            this.probaRepo = probaRepo;
            this.istoricRepo= istoricRepo;
        }

        public List<ProbaModel?> GetProbe()
        {
            var probe = probaRepo.GetProbeIQueryable();
            if (probe == null)
            {
                return new List<ProbaModel>();
            }
            var probeModel = probe
                .Select(x => new ProbaModel
                {
                    numeProba = x.numeProba
                })
                .OrderBy(x => x.numeProba)
                .ToList();
            if(probeModel.Count> 0) { return probeModel; }
            return probeModel;       
        }

        public List<ProbaModelTotal> GetProbeTotal()
        {
            var probe = probaRepo.GetProbeIQueryable();
            if (probe == null)
            {
                return new List<ProbaModelTotal>();
            }
            var probeModel = probe
                .Select(x => new ProbaModelTotal
                {
                    codProba = x.codProba,
                    numeProba = x.numeProba,
                    timpLimita = x.timpLimita,
                    detaliiDistante = x.detaliiDistante
                })
                .OrderBy(x => x.numeProba)
                .ToList();
            if (probeModel.Count > 0) { return probeModel; }
            return probeModel;
        }


        public List<ProbaModelById> GetProbaInfo(int id)
        {
            var probe = probaRepo.GetProbeIQueryable()
                .Where(x => x.codProba == id)
                .Select(x => new ProbaModelById
                {
                    numeProba = x.numeProba,
                    timpLimita = x.timpLimita,
                    detaliiDistante= x.detaliiDistante
                })
                .ToList();
            return probe;
        }

        public List<ProbaModelComp> GetProbaCompetitie(int id)
        {
            var probe = istoricRepo.GetIstoricProbaCompetitie()
                .Where(x => x.codCompetitie == id)
                .Select(x => new ProbaModelComp
                {
                    numeProba = x.Proba.numeProba,
                    timpLimita = x.Proba.timpLimita
                })
                .Distinct()
                .ToList();
            return probe;
        }

        public List<int> GetNrParticipProbeComp(int id)
        {
            var probe = istoricRepo.GetIstoricProbaCompetitie()
                .Where(x => x.codCompetitie == id)
                .GroupBy(x => x.codProba)
                .Select(x => x.Count())
                .ToList();
            return probe;
        }
       

        public void Delete(int id)
        {
            var proba = probaRepo.GetProbeIQueryable()
                .FirstOrDefault(x => x.codProba == id);
            if(proba == null)
            { return; }
            probaRepo.Delete(proba);
        }

        public void Create(ProbaModelById probaModelById)
        {
            var newProba = new Proba
            {
                numeProba = probaModelById.numeProba,
                timpLimita = probaModelById.timpLimita,
                detaliiDistante = probaModelById.detaliiDistante
            };
            probaRepo.Create(newProba);
        }

        public void Update(ProbaUpdateModel probaUpdateModel)
        {
            var proba = probaRepo.GetProbeIQueryable()
                .FirstOrDefault(x => x.codProba == probaUpdateModel.codProba);
            if (proba == null)
                return;
            proba.timpLimita = probaUpdateModel.timpLimita;
            probaRepo.Update(proba);
        }

    }
}
