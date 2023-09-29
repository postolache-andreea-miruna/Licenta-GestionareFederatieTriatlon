using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Migrations;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;

namespace GestionareFederatieTriatlon.Manageri
{
    public class TipManager : ITipManager
    {
        private readonly ITipRepo tipRepo;
        public TipManager(ITipRepo tipRepo)
        {
            this.tipRepo = tipRepo;
        }
        public List<TipModelTotal?> GetTipuri()
        {
            var tipuri = tipRepo.GetTipIQueryable();
            if (tipuri == null)
            {
                return new List<TipModelTotal>();
            }
            var tipuriModel = tipuri
                .Select(t => new TipModelTotal
                {
                    numarMinimParticipanti = t.numarMinimParticipanti,
                    codTip = t.codTip,
                    tipCompetitie = t.tipCompetitie
                })
                .OrderBy(t => t.tipCompetitie)
                .ToList();
            if (tipuriModel.Count > 0) { return tipuriModel; }
            return tipuriModel;
        }
        public List<TipModel?> GetTip()
        {
            var tipuri = tipRepo.GetTipIQueryable();
            if (tipuri == null)
            {
                return new List<TipModel>();
            }
            var tipuriModel = tipuri
                .Select(t => new TipModel
                {
                    tipCompetitie = t.tipCompetitie
                })
                .OrderBy(t =>t.tipCompetitie)
                .ToList();
            if(tipuriModel.Count> 0) { return tipuriModel; }
            return tipuriModel;
        }

        public List<TipModelById> GetTipInfo(int id)
        {
            var tipuri = tipRepo.GetTipIQueryable()
                .Where(t => t.codTip == id)
                .Select(t => new TipModelById
                {
                    tipCompetitie = t.tipCompetitie,
                    numarMinimParticipanti= t.numarMinimParticipanti
                })
                .ToList();
            return tipuri;
        }

        public List<TipModelCodNume> GetTipCodNume()
        {
            var tipuri = tipRepo.GetTipIQueryable()
                .Select(t => new TipModelCodNume
                {
                    codTip = t.codTip,
                    tipCompetitie = t.tipCompetitie
                })
                .ToList();
            return tipuri;
        }

        public void Create(TipModelById model)
        {
            var newTip = new Tip
            {
                tipCompetitie = model.tipCompetitie,
                numarMinimParticipanti= model.numarMinimParticipanti
            };
            tipRepo.Create(newTip);
        }

        public void Update(TipUpdateModel tipUpdateModel)
        {
            var tip = tipRepo.GetTipIQueryable()
                .FirstOrDefault(t => t.codTip == tipUpdateModel.codTip);
            if (tip == null)
                return;
            tip.numarMinimParticipanti = tipUpdateModel.numarMinimParticipanti;
            tipRepo.Update(tip);
        }

        public void Delete(int id)
        {
            var tip = tipRepo.GetTipIQueryable()
                .FirstOrDefault(t => t.codTip == id);
            if(tip == null) return;
            tipRepo.Delete(tip);
        }


    }
}
