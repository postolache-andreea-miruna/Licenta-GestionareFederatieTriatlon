using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionareFederatieTriatlon.Manageri
{
    public class CompetitieManager: ICompetitieManager
    {
        private GestionareFederatieTriatlonContext db;
     
        private readonly ICompetitieRepo competitieRepo;
        private readonly IIstoricRepo istoricRepo;
        private readonly UserManager<Utilizator> utilizatorManager;
        public CompetitieManager(ICompetitieRepo competitieRepo, IIstoricRepo istoricRepo, UserManager<Utilizator> utilizatorManager, GestionareFederatieTriatlonContext db)
        {
            this.competitieRepo = competitieRepo;
            this.istoricRepo = istoricRepo;
            this.utilizatorManager= utilizatorManager;
            this.db = db;
        }
        public List<CompetitieModelTotal?> GetCompetitiiTotal()
        {
            var competitii = competitieRepo.GetCompetitiiIQueryable();
            if (competitii == null)
            {
                return new List<CompetitieModelTotal>();
            }

            var competitiiModel = competitii
                .Select(c => new CompetitieModelTotal
                {
                    codCompetitie = c.codCompetitie,
                    numeCompetitie = c.numeCompetitie,
                    taxaParticipare= c.taxaParticipare,
                    statusCompetitie = c.statusCompetitie,
                    dataStart = c.dataStart,
                    dataFinal = c.dataFinal,
                    paginaOficialaCompetitie = c.paginaOficialaCompetitie,
                    codLocatie = c.codLocatie,
                    tipCompetitie = c.Tip.tipCompetitie
                })
                .OrderByDescending(c => c.dataStart)
                .ToList();
            if (competitiiModel.Count > 0) { return competitiiModel; }

            return competitiiModel;
        }

        public List<CompetitieModel?> GetCompetitii()
        {
            var competitii = competitieRepo.GetCompetitiiIQueryable();
            if (competitii == null)
            {
                return new List<CompetitieModel>();
            }

            var competitiiModel = competitii
                .Select(c => new CompetitieModel
                {
                    codCompetitie = c.codCompetitie,
                    statusCompetitie = c.statusCompetitie,
                    numeCompetitie = c.numeCompetitie,
                    dataStart = c.dataStart,
                    dataFinal = c.dataFinal,
                    tipCompetitie = c.Tip.tipCompetitie
                })
                .OrderByDescending(c => c.dataStart)
                .ToList();
            if(competitiiModel.Count > 0) { return competitiiModel; }

            return competitiiModel;
        }

        public List<CompetitieNumeIdModel?> GetNumeIdCompetitii()
        {
            var competitii = competitieRepo.GetCompetitiiIQueryable();
            if (competitii == null)
            {
                return new List<CompetitieNumeIdModel>();
            }

            var competitiiModel = competitii
                //.Where !db.Videoclipuri.Select(v => v.codCompetitie).Contains(c =>c.codCompetitie)
                .Where(c => !db.Videoclipuri.Any(v => v.codCompetitie == c.codCompetitie) && c.statusCompetitie.Equals("activa"))
                .Select(c => new CompetitieNumeIdModel
                {
                    codCompetitie = c.codCompetitie,
                    numeCompetitie = c.numeCompetitie,
                })
                .ToList();
            if (competitiiModel.Count > 0) { return competitiiModel; }

            return competitiiModel;
        }
        public CompetitieNume GetNumeCompId(int codCompetitie)
        {
            var nume = competitieRepo.GetCompetitiiIQueryable()
                .Where(c => c.codCompetitie.Equals(codCompetitie))
                .Select(c=> new CompetitieNume
                {
                    numeCompetitie= c.numeCompetitie
                })
                .FirstOrDefault();
            return nume;
        }
        public List<CompetitieNume?> GetNumeCompNeanulate()
        {
            var competitii = competitieRepo.GetCompetitiiIQueryable();
            if (competitii == null)
            {
                return new List<CompetitieNume>();
            }

            var competitiiModel = competitii
                .Where(c => c.statusCompetitie.Equals("activa"))
                .Select(c => new CompetitieNume
                {
                    numeCompetitie = c.numeCompetitie,
                   
                })
                .ToList();
            if (competitiiModel.Count == 0) { return new List<CompetitieNume>(); }

            return competitiiModel;
        }

        public List<CompetitieModel> GetCompetitiiSportiv(string email)
        {
            var utilizatori = utilizatorManager.Users;
            var codSportiv = utilizatori.Where(u => u.Email.Equals(email)).Select(u => u.Id).FirstOrDefault();
            if (codSportiv == null)
            { return new List<CompetitieModel>(); }

            var competitii = istoricRepo.GetIstoricProbaCompetitieSportiv()  //competitiile la care participa sportivul cu email dat
                .Where(c => c.codUtilizator.Equals(codSportiv))
                 .Select(c => new CompetitieModel
                 {
                     codCompetitie = c.codCompetitie,
                     statusCompetitie = c.Competitie.statusCompetitie,
                     numeCompetitie = c.Competitie.numeCompetitie,
                     dataStart = c.Competitie.dataStart,
                     dataFinal = c.Competitie.dataFinal,
                     tipCompetitie = c.Competitie.Tip.tipCompetitie
                 })
                 .Distinct()
                 .OrderByDescending(c => c.dataStart)
                 .ToList();
            if (competitii == null)
            { return new List<CompetitieModel>(); }

            return competitii;
        }


        public List<CompetitieModelById> GetCompetitieInfo(int id) //aici ar trebui facut join cu videoclip si sa se ia linkul din el
        {
            var competitie = competitieRepo.GetCompetitiiIQueryable()
                .Where(c => c.codCompetitie == id)
                .Select(c => new CompetitieModelById
                {
                    numeCompetitie = c.numeCompetitie,
                    dataStart= c.dataStart,
                    dataFinal= c.dataFinal,
                    taxaParticipare = c.taxaParticipare,
                    paginaOficialaCompetitie = c.paginaOficialaCompetitie,
                    tipCompetitie = c.Tip.tipCompetitie,
                    numarMinimParticipanti = c.Tip.numarMinimParticipanti
                })
                .ToList();
            return competitie;
        }

/*        public void Delete(CompetitieModelDelete modelDelete) //a sterge o competitie = a face update la status = anulata
        {
            var competitie = competitieRepo.GetCompetitiiIQueryable()
                .FirstOrDefault(x => x.codCompetitie == modelDelete.codCompetitie);

            if (competitie == null)
                return;
            competitie.statusCompetitie = "anulata";
            competitieRepo.Update(competitie);

        }*/

        public void Delete(int id) //a sterge o competitie = a face update la status = anulata
        {
            var competitie = competitieRepo.GetCompetitiiIQueryable()
                .FirstOrDefault(x => x.codCompetitie == id);

            if (competitie == null)
                return;
            competitie.statusCompetitie = "anulata";
            competitieRepo.Update(competitie);

        }

        public void Update(CompetitieModelUpdate modelUpdate)
        {
            var competitie = competitieRepo.GetCompetitiiIQueryable()
               .FirstOrDefault(x => x.codCompetitie == modelUpdate.codCompetitie);

            if(competitie == null)
                return;
            competitie.numeCompetitie = modelUpdate.numeCompetitie;
            competitie.taxaParticipare = modelUpdate.taxaParticipare;
            competitie.dataStart = modelUpdate.dataStart;
            competitie.dataFinal = modelUpdate.dataFinal;
            competitie.paginaOficialaCompetitie = modelUpdate.paginaOficialaCompetitie;
            competitie.statusCompetitie = modelUpdate.statusCompetitie;
            competitie.codLocatie = modelUpdate.codLocatie;

            competitieRepo.Update(competitie);
        }

        public void Create(CompetitieModelCreate modelCreate)
        {
            var newCompetitie = new Competitie
            {
                numeCompetitie = modelCreate.numeCompetitie,
                taxaParticipare = modelCreate.taxaParticipare,
                dataStart = modelCreate.dataStart,
                dataFinal = modelCreate.dataFinal,
                paginaOficialaCompetitie = modelCreate.paginaOficialaCompetitie,
                statusCompetitie = modelCreate.statusCompetitie,
                codTip = modelCreate.codTip,
                codLocatie = modelCreate.codLocatie
            };
            competitieRepo.Create(newCompetitie);
        }
    }
}
