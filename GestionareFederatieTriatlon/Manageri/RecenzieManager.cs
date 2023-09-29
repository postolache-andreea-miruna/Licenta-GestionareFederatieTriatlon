using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionareFederatieTriatlon.Manageri
{
    public class RecenzieManager: IRecenzieManager
    {
        private readonly IRecenzieRepo recenzieRepo;
        private readonly IIstoricRepo istoricRepo;
        private readonly ICompetitieRepo compRepo;

        private readonly UserManager<Utilizator> utilizatorManager;
        public RecenzieManager(IRecenzieRepo recenzieRepo, IIstoricRepo istoricRepo, ICompetitieRepo compRepo, UserManager<Utilizator> utilizatorManager)
        {
            this.recenzieRepo = recenzieRepo;
            this.istoricRepo = istoricRepo;
            this.compRepo = compRepo;
            this.utilizatorManager = utilizatorManager;
        }
        public List<RecenzieCodTextModel> GetCodTextRecenzii()
        {
            var recenzii = recenzieRepo.GetRecenzii()
                .Select(r => new RecenzieCodTextModel
                {
                    codRecenzie = r.codRecenzie,
                    text = r.text
                })
                .OrderBy(r => r.codRecenzie)
                .ToList();
            return recenzii;
        }

        public bool RecenzieDataSportivCompId(string email, int idComp)
        {
            var utilizatori = utilizatorManager.Users;
            var codSportiv = utilizatori.Where(u => u.Email.Equals(email)).Select(u => u.Id).FirstOrDefault();

            if (codSportiv == null) { return false; }

            var recenzare = recenzieRepo.GetRecenzii()
                            .Where(r => r.codCompetitie.Equals(idComp) && r.codUtilizator.Equals(codSportiv))
                            .Count();

            if (recenzare == 0)
            {
                return true;
            }
            else 
            {
                return false;
            }

        }

        public double GetCompetitieSteleMedie(int id)//id competitie
        {
            var recenzie = recenzieRepo.GetRecenzii()
                .Where(r => r.codCompetitie.Equals(id));
            if (recenzie.Count() == 0 )
            {
                return 0;
            }
            else
            {
                double recenzieMedie = (double)recenzie.Average(c => c.numarStele);
                if(recenzieMedie < 0.5)
                {
                    recenzieMedie = 0;
                }
                else if(recenzieMedie >= 0.5 && recenzieMedie < 1.5)
                {
                    recenzieMedie = 1;
                }
                else if (recenzieMedie >= 1.5 && recenzieMedie < 2.5)
                {
                    recenzieMedie = 2;
                }
                else if (recenzieMedie >= 2.5 && recenzieMedie < 3.5)
                {
                    recenzieMedie = 3;
                }
                else if (recenzieMedie >= 3.5 && recenzieMedie < 4.5)
                {
                    recenzieMedie = 4;
                }
                else if (recenzieMedie >= 4.5 && recenzieMedie <= 5)
                {
                    recenzieMedie = 5;
                }
                return recenzieMedie;
            }
        }

        public List<RecenziiSportiviModel> RecenziiSportiviCompId(int id)//id competitie
        {
            var recenzii = recenzieRepo.GetRecenzii()
                .Include(s => s.Sportiv)
                .Where(r => r.codCompetitie.Equals(id))
                .Select(r => new RecenziiSportiviModel
                {
                    nume = r.Sportiv.nume,
                    prenume = r.Sportiv.prenume,
                    urlPozaProfil = r.Sportiv.urlPozaProfil,
                    numarStele = r.numarStele,
                    text = r.text,
                    completareRecenzie = r.completareRecenzie
                })
                .OrderByDescending(r => r.numarStele) 
                .ToList();

            return recenzii;
        }


        public List<RecenziileSportivModel> RecenziiSportiv(string email)
        {
            var utilizatori = utilizatorManager.Users;
            var codSportiv = utilizatori.Where(u => u.Email.Equals(email)).Select(u => u.Id).FirstOrDefault();

            var competitie = compRepo.GetCompetitiiIQueryable();

            var recenzii = recenzieRepo.GetRecenzii()
                .Where(r => r.codUtilizator.Equals(codSportiv))
                .Select(r => new RecenziileSportivModel
                {
                    numarStele = r.numarStele,
                    text = r.text,
                    completareRecenzie = r.completareRecenzie,
                    numeCompetitie = competitie.Where(c => c.codCompetitie.Equals(r.codCompetitie)).Select(c => c.numeCompetitie).FirstOrDefault(),
                    codRecenzie = r.codRecenzie
                })
                .OrderByDescending(r => r.completareRecenzie)
                .ToList();

            return recenzii;
        }
        /* public void Create(FormularCreateModel formCreate)
         {
             var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == formCreate.numarLegitimatie);
             if (sportiv == null)
                 return;
             var newForm = new Formular
             {
                 pozaProfil = formCreate.pozaProfil,
                 avizMedical = formCreate.avizMedical,
                 buletin_CertificatNastere = formCreate.buletin_CertificatNastere,
                 codUtilizator = sportiv.Id
             };
             repo.Create(newForm);
         }*/
        /* public void Create(RecenzieCreateModel createModel)
         {
             //trebuie verificat daca sportivul a participat la competitia la care va da o recenzie
             var codComp = createModel.codCompetitie;
             var codSportiv = createModel.codUtilizator;
             var participare = istoricRepo.GetIstoricProbaCompetitieSportiv()
                 .Where(c => c.codCompetitie.Equals(codComp) && c.codUtilizator.Equals(codSportiv))
                 .Count();
             if (participare == 0)
                 return;
             var newRecenzie = new Recenzie
             {
                 numarStele = createModel.numarStele,
                 text = createModel.text,
                 completareRecenzie = DateTime.Now,
                 codUtilizator = createModel.codUtilizator,
                 codCompetitie = createModel.codCompetitie
             };
             recenzieRepo.Create(newRecenzie);

         }*/

        public void Create(RecenzieCreateModel createModel)
        {
            //trebuie verificat daca sportivul a participat la competitia la care va da o recenzie
            var codComp = createModel.codCompetitie;

            //trebuie sa gasim sportivul cu emailul dat
            var utilizatori = utilizatorManager.Users;
            var codSportiv = utilizatori.Where(u => u.Email.Equals(createModel.emailUtilizator)).Select(u => u.Id).FirstOrDefault();

            var participare = istoricRepo.GetIstoricProbaCompetitieSportiv()
                .Where(c => c.codCompetitie.Equals(codComp) && c.codUtilizator.Equals(codSportiv))
                .Count();
            if (participare == 0)
                return;
            var newRecenzie = new Recenzie
            {
                numarStele = createModel.numarStele,
                text = createModel.text,
                completareRecenzie = DateTime.Now,
                codUtilizator = codSportiv,
                codCompetitie = createModel.codCompetitie
            };
            recenzieRepo.Create(newRecenzie);

        }

        public void Update(RecenzieUpdateModel updateModel)
        {
            var recenzie = recenzieRepo.GetRecenzii()
                .FirstOrDefault(r => r.codRecenzie == updateModel.codRecenzie);

            if (recenzie == null)
                return;

            recenzie.text = updateModel.text;
            recenzieRepo.Update(recenzie);
        }

        public void Delete(int id)
        {

            var recenzie = recenzieRepo.GetRecenzii()
                .FirstOrDefault(r => r.codRecenzie == id);

            if (recenzie == null)
                return;

            recenzieRepo.Delete(recenzie);
        }
    }
}
