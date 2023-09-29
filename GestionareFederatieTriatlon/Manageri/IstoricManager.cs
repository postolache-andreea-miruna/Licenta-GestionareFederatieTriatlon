using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace GestionareFederatieTriatlon.Manageri
{
    public class IstoricManager : IIstoricManager
    {
        private readonly IIstoricRepo repo;
        private readonly ICompetitieRepo repoComp;
        private readonly IIstoricClubRepo repoIstCl;
        private readonly IClubRepo repoCl;
        
        private readonly UserManager<Utilizator> utilizatorManager;
        public IstoricManager(IClubRepo repoCl, IIstoricRepo repo, ICompetitieRepo repoComp, IIstoricClubRepo repoIstCl, UserManager<Utilizator> utilizatorManager)
        {
            this.repoCl = repoCl;
            this.repo = repo;
            this.repoComp = repoComp;
            this.repoIstCl = repoIstCl;
            this.utilizatorManager = utilizatorManager;
        }

        public List<string> GetNumeProbe(int id)//id comp
        {
            var nume = repo.GetIstoricProbaCompetitieSportiv()
                                .Where(i => i.codCompetitie == id)
                                .Select(i => i.Proba.numeProba)
                                .Distinct()
                                .ToList();
            return nume;
        }

        public List<string> GetNumeCompetitie(int id)//id comp
        {
            var nume = repoComp.GetCompetitiiIQueryable()
                                .Where(i => i.codCompetitie == id)
                                .Select(i => i.numeCompetitie)
                                .ToList();
            return nume;
        }

        public List<string> GetCategParticipante(int id)//id comp
        {
            var categ = repo.GetIstoricProbaCompetitieSportiv()
                                .Where(i => i.codCompetitie == id)
                                .Select(i => i.categorie)
                                .Distinct()
                                .ToList();
            return categ;
        }
        /*        public List<string> GetCluburiParticipante(int id)//id comp
                {
                    var nume = repo.GetIstoricProbaCompetitieSportiv()
                                        .Where(i => i.codCompetitie == id)
                                        .Select(i => i.Sportiv.Club.nume)
                                        .Distinct()
                                        .ToList();
                    return nume;
                }*/
        public List<IstoricCreateModel> GetIstoriceForCompetitieId(int codCompetitie)
        {
            var competitii = repo.GetIstoricProbaCompetitieSportiv()
                .Where(i => i.codCompetitie == codCompetitie)
                .Select(i => new IstoricCreateModel
                {
                    numarLegitimatie = i.Sportiv.numarLegitimatie,
                    numeProba = i.Proba.numeProba,
                    numeCompetitie = i.Competitie.numeCompetitie,
                    categorie = i.categorie,
                    locPesteToti = i.locPesteToti,
                    locPerGen = i.locPerGen,
                    locPerCategorie = i.locPerCategorie,
                    timpTotal = i.timpTotal,
                    timpInot = i.timpInot,
                    timpCiclism = i.timpCiclism,
                    timpAlergare = i.timpAlergare,
                    timpTranzit1 = i.timpTranzit1,
                    timpTranzit2 = i.timpTranzit2,
                    puncte = i.puncte
                })
                .OrderBy(i => i.numeProba)
                .ThenBy(i=>i.categorie)
                .ToList();
            return competitii;
        }
        public List<string> GetCluburiParticipante(int id)//id comp
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
                .Where(c => c.codCompetitie == id)
                .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;


            var sportivi = repo.GetIstoricProbaCompetitieSportiv() //codurile sportivilor care au participat la comp
                                .Where(i => i.codCompetitie == id)
                                .Select(i => i.codUtilizator)
                                .Distinct()
                                .ToList();

            var numeCluburi = new List<string>();

            for (int i = 0; i < sportivi.Count(); i++)
            {
                //trebuie sa stim sportivii cu id dat
                var sportiv = repo.GetSportivi()
                    .Where(s => s.Id == sportivi[i])
                    .FirstOrDefault();
                    /*.Where(s => s.Id == sportivi[i])
                    .FirstOrDefault();*/

                if(sportiv.dataInscreireClubActual > dataStart) //daca sportivul nu era inscris la clubul actual cand a fost competitia respectiva
                {
                    //atunci trebuie cautat in istoric
                    var numeClub = repoIstCl.GetIstoricCluburi()
                        .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == sportivi[i])
                        .Select(ic => ic.Club.nume)
                        .FirstOrDefault();

                    numeCluburi.Add(numeClub);
                }
                else
                {
                    var numeClub = repoCl.GetCluburiIQueryable()
                        .Where(c => c.codClub == sportiv.codClub)
                        .Select(c => c.nume) .FirstOrDefault();
                    
                   // var numeClub = sportiv.Club.nume;
                    numeCluburi.Add(numeClub);
                }

            }
            IEnumerable<string> NumeDistincte = numeCluburi.Distinct(); 
            List<string> listaNumeDistincte = new List<string>(NumeDistincte); 

            return listaNumeDistincte;
        }
        public void Create(IstoricCreateModel model)
        {
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == model.numarLegitimatie);

            var proba = repo.GetProbe().FirstOrDefault(i => i.numeProba == model.numeProba);
            var comp = repo.GetCompetitii().FirstOrDefault(i => i.numeCompetitie == model.numeCompetitie);

            if (sportiv == null || proba == null || comp == null)
                return;

            var newIstoric = new Istoric
            {
                codUtilizator = sportiv.Id,//vreau codul utilizatorului care are codullegitimatie cel dat de mine
                codProba = proba.codProba,
                codCompetitie = comp.codCompetitie,
                categorie = model.categorie,
                locPesteToti = model.locPesteToti,
                locPerGen = model.locPerGen,
                locPerCategorie = model.locPerCategorie,
                timpTotal = model.timpTotal,
                timpInot = model.timpInot,
                timpCiclism = model.timpCiclism,
                timpAlergare = model.timpAlergare,
                timpTranzit1 = model.timpTranzit1,
                timpTranzit2 = model.timpTranzit2,
                puncte = model.puncte
            };
            repo.Create(newIstoric);
        }

        public void Update(IstoricCreateModel model)
        {

            var istoricSp = repo.GetIstoricProbaCompetitieSportiv().FirstOrDefault(i => i.Sportiv.numarLegitimatie == model.numarLegitimatie);
            var istoricPr = repo.GetIstoricProbaCompetitie().FirstOrDefault(i => i.Proba.numeProba == model.numeProba);
            var istoricComp = repo.GetIstoricProbaCompetitie().FirstOrDefault(i => i.Competitie.numeCompetitie == model.numeCompetitie);

            if (istoricSp == null || istoricPr == null || istoricComp == null)
                return;

            var istoric = repo.GetIstoricProbaCompetitie()
                .FirstOrDefault(x => x.codUtilizator == istoricSp.codUtilizator &&
                                     x.codProba == istoricPr.codProba &&
                                     x.codCompetitie == istoricComp.codCompetitie);
            if(istoric == null)
                return;

            istoric.codUtilizator = istoricSp.codUtilizator;
            istoric.codProba = istoricPr.codProba;
            istoric.codCompetitie = istoricComp.codCompetitie;
            istoric.categorie = model.categorie;
            istoric.locPesteToti = model.locPesteToti;
            istoric.locPerCategorie = model.locPerCategorie;
            istoric.locPerGen = model.locPerGen;
            istoric.timpTotal = model.timpTotal;
            istoric.timpInot = model.timpInot;
            istoric.timpAlergare = model.timpAlergare;
            istoric.timpCiclism = model.timpCiclism;
            istoric.timpTranzit1 = model.timpTranzit1;
            istoric.timpTranzit2 = model.timpTranzit2;
            istoric.puncte = model.puncte;

            repo.Update(istoric);
        }

        /*public List<IstoricSportivBestOfModel> GetBestOfByIdSportiv(string id)
        {
            var istorice = repo.GetIstoricProbaCompetitie()
                               .Where(i => i.codUtilizator == id)
                               .GroupBy(p => p.Proba.numeProba)
                               .Select(i => new IstoricSportivBestOfModel
                               {
                                   numeProba = i.Key,
                                   numeCompetitie = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.numeCompetitie,
                                   timpTotal = i.Min(time => time.timpTotal),

                                   timpInot = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpInot,

                                   timpCiclism = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpCiclism,

                                   timpAlergare = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpAlergare,

                                   timpTranzit1 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit1,

                                   timpTranzit2 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit2
                               })
                               .ToList();
            return istorice;
        }*/

        public List<IstoricSportivBestOfModel> GetBestOfByEmailSportiv(string email)
        {
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.Email == email); //sportivul cu email-ul dat
            if(sportiv == null)
            {
                return new List<IstoricSportivBestOfModel>();
            }
            var istorice = repo.GetIstoricProbaCompetitie()
                               .Where(i => i.codUtilizator == sportiv.Id)
                               .GroupBy(p => p.Proba.numeProba)
                               .Select(i => new IstoricSportivBestOfModel
                               {
                                   numeProba = i.Key,
                                   numeCompetitie = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.numeCompetitie,
                                   dataStart = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.dataStart,
                                   dataFinal = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.dataFinal,
                                   timpTotal = i.Min(time => time.timpTotal),

                                   timpInot = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpInot,

                                   timpCiclism = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpCiclism,

                                   timpAlergare = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpAlergare,

                                   timpTranzit1 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit1,

                                   timpTranzit2 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit2
                               })
                               .ToList();
            return istorice;
        }

        public List<IstoricSportivBestOfModel> GetBestOfByNrLegitimatieSportiv(int numarLegitimatie)
        {
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == numarLegitimatie); //sportivul cu legitimatie data
            if (sportiv == null)
            {
                return new List<IstoricSportivBestOfModel>();
            }
            var istorice = repo.GetIstoricProbaCompetitie()
                               .Where(i => i.codUtilizator == sportiv.Id)
                               .GroupBy(p => p.Proba.numeProba)
                               .Select(i => new IstoricSportivBestOfModel
                               {
                                   numeProba = i.Key,
                                   numeCompetitie = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.numeCompetitie,
                                   dataStart = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.dataStart,
                                   dataFinal = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                    .FirstOrDefault().Competitie.dataFinal,
                                   timpTotal = i.Min(time => time.timpTotal),

                                   timpInot = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpInot,

                                   timpCiclism = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpCiclism,

                                   timpAlergare = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpAlergare,

                                   timpTranzit1 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit1,

                                   timpTranzit2 = i.Where(time => time.timpTotal == i.Min(t => t.timpTotal))
                                   .FirstOrDefault().timpTranzit2
                               })
                               .ToList();
            return istorice;
        }

        public List<IstoricSportivModel> GetIstoricRezultateByIdSportiv(string id)
        {
            var istorice = repo.GetIstoricProbaCompetitie()
                               .Where(i => i.codUtilizator == id)
                               .Select(i => new IstoricSportivModel
                               {
                                   numeCompetitie = i.Competitie.numeCompetitie,
                                   dataStart = i.Competitie.dataStart,
                                   dataFinal = i.Competitie.dataFinal,

                                   numeProba = i.Proba.numeProba,
                                   timpTotal = i.timpTotal,
                                   timpInot = i.timpInot,
                                   timpCiclism = i.timpCiclism,
                                   timpAlergare = i.timpAlergare,
                                   timpTranzit1 = i.timpTranzit1,
                                   timpTranzit2 = i.timpTranzit2,

                                   locPesteToti = i.locPesteToti,
                                   locPerGen = i.locPerGen,
                                   locPerCatetogrie = i.locPerCategorie,

                                   puncte = i.puncte
                               })
                               .OrderByDescending(i => i.dataFinal)//.OrderByDescending(i => i.numeCompetitie)//.OrderBy(i => i.numeCompetitie)
                               .ToList();
            return istorice;

        }

        public List<IstoricSportivModel> GetIstoricRezultateByLegitimatieSportiv(int numarLegitimatie)
        {
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == numarLegitimatie); //sportivul cu legitimatie data
            if (sportiv == null)
            {
                return new List<IstoricSportivModel>();
            }
            var istorice = repo.GetIstoricProbaCompetitie()
                               .Where(i => i.codUtilizator == sportiv.Id)
                               .Select(i => new IstoricSportivModel
                               {
                                   numeCompetitie = i.Competitie.numeCompetitie,
                                   dataStart = i.Competitie.dataStart,
                                   dataFinal = i.Competitie.dataFinal,

                                   numeProba = i.Proba.numeProba,
                                   timpTotal = i.timpTotal,
                                   timpInot = i.timpInot,
                                   timpCiclism = i.timpCiclism,
                                   timpAlergare = i.timpAlergare,
                                   timpTranzit1 = i.timpTranzit1,
                                   timpTranzit2 = i.timpTranzit2,

                                   locPesteToti = i.locPesteToti,
                                   locPerGen = i.locPerGen,
                                   locPerCatetogrie = i.locPerCategorie,

                                   puncte = i.puncte
                               })
                               .OrderByDescending(i => i.dataFinal)//.OrderByDescending(i => i.numeCompetitie)//.OrderBy(i => i.numeCompetitie)
                               .ToList();
            return istorice;

        }

        /*        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitie(int id) //ordonate pe probe si ordonati in ordinea locului obtinut pe open
                {
                    var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                        .Where(i => i.codCompetitie == id)
                                        .Select(i => new IstoricRezultateCompetitieModel
                                        {
                                            numeSportiv = i.Sportiv.nume,
                                            prenumeSportiv = i.Sportiv.prenume,
                                            categorie = i.categorie,
                                            numeClub = i.Sportiv.Club.nume,
                                            numeProba = i.Proba.numeProba,
                                            timpTotal = i.timpTotal,
                                            locPesteToti = i.locPesteToti,
                                            locPerGen = i.locPerGen,
                                            locPerCatetogrie = i.locPerCategorie,
                                            puncte = i.puncte
                                        })
                                        .OrderBy(i => i.numeProba)
                                        .ThenBy(i => i.locPesteToti)
                                        .ToList();
                    return istorice;
                }*/
        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitie(int id) //ordonate pe probe si ordonati in ordinea locului obtinut pe open
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
               .Where(c => c.codCompetitie == id)
               .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;

                var istoricele = repo.GetIstoricProbaCompetitieSportiv()
                                .Where(i => i.codCompetitie == id)
                                .Select(i => new IstoricRezultateCompetitieModel
                                {
                                    numeSportiv = i.Sportiv.nume,
                                    prenumeSportiv = i.Sportiv.prenume,
                                    categorie = i.categorie,
                                    numeClub = i.Sportiv.dataInscreireClubActual > dataStart ?
                                           repoIstCl.GetIstoricCluburi()
                                            .Where(ic => ic.dataInscriereClub <= dataStart &&
                                                    ic.dataParasireClub >= dataFinal &&
                                                    ic.codUtilizator == i.codUtilizator)
                                            .Select(ic => ic.Club.nume)
                                            .FirstOrDefault() :
                                            i.Sportiv.Club.nume,
                                    numeProba = i.Proba.numeProba,
                                    timpTotal = i.timpTotal,
                                    locPesteToti = i.locPesteToti,
                                    locPerGen = i.locPerGen,
                                    locPerCatetogrie = i.locPerCategorie,
                                    puncte = i.puncte
                                })
                                .OrderBy(i => i.numeProba)
                                .ThenBy(i => i.locPesteToti)
                                .ToList();
            return istoricele;
        }

        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitieNumeProba(int id, string numeProba = "toate probele")
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
             .Where(c => c.codCompetitie == id)
             .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;

            if (numeProba == "toate probele")
            {
                var istorice = GetRezultateCompetitie(id);
                return istorice;
            }
            else
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                    .Where(i => i.Proba.numeProba == numeProba)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ?
                                           repoIstCl.GetIstoricCluburi()
                                            .Where(ic => ic.dataInscriereClub <= dataStart &&
                                                    ic.dataParasireClub >= dataFinal &&
                                                    ic.codUtilizator == i.codUtilizator)
                                            .Select(ic => ic.Club.nume)
                                            .FirstOrDefault() :
                                            i.Sportiv.Club.nume,
                                        //numeClub = i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .OrderBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;
            }
        }
        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitieClub(int id, string numeClub = "toate cluburile") //rezolvat
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
              .Where(c => c.codCompetitie == id)
              .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;

            if (numeClub == "toate cluburile")
            {
                var istorice = GetRezultateCompetitie(id);
                return istorice;
            }
            else
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                    //.Where(i => i.Sportiv.Club.nume == numeClub)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ? repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == i.codUtilizator && ic.Club.nume == numeClub)
                            .Select(ic => ic.Club.nume)
                            .FirstOrDefault() :
                            (i.Sportiv.Club.nume == numeClub ? numeClub : null),
                                        //numeClub = i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .Where(i => i.numeClub != null)
                                    .OrderBy(i => i.numeProba)
                                    .ThenBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;
            }
        }
        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitieCategorie(int id, string categorie = "toate categoriile")
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
              .Where(c => c.codCompetitie == id)
              .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;
            if (categorie == "toate categoriile")
            {
                var istorice = GetRezultateCompetitie(id);
                return istorice;
            }
            else
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                    .Where(i => i.categorie == categorie)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ?
                                           repoIstCl.GetIstoricCluburi()
                                            .Where(ic => ic.dataInscriereClub <= dataStart &&
                                                    ic.dataParasireClub >= dataFinal &&
                                                    ic.codUtilizator == i.codUtilizator)
                                            .Select(ic => ic.Club.nume)
                                            .FirstOrDefault() :
                                            i.Sportiv.Club.nume,
                                        // numeClub = i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .OrderBy(i => i.numeProba)
                                    .ThenBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;
            }
        }


        public List<IstoricRezultateCompetitieModel> GetRezultateCompetitieNumeProbaCategorieClub(int id, string numeProba = "toate probele", string categorie = "toate categoriile", string club = "toate cluburile")
        {
            var competitie = repoComp.GetCompetitiiIQueryable()
                .Where(c => c.codCompetitie == id)
                .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;

            if (numeProba == "toate probele" && categorie == "toate categoriile" && club == "toate cluburile")
            {
                var istorice = GetRezultateCompetitie(id);//REZOLVAT
                return istorice;
            }
            else if (numeProba == "toate probele" && categorie != "toate categoriile" && club != "toate cluburile")// categorie & club FACUT
            {

                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                   // .Where(i => i.Sportiv.Club.nume == club)
                                    .Where(i => i.categorie == categorie)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ? repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == i.codUtilizator && ic.Club.nume == club)
                            .Select(ic => ic.Club.nume)
                            .FirstOrDefault() :
                            (i.Sportiv.Club.nume == club ? club :null),// i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .Where(i => i.numeClub != null)
                                    .OrderBy(i => i.numeProba)
                                    .ThenBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;

            }
            else if (numeProba != "toate probele" && categorie == "toate categoriile" && club != "toate cluburile")// proba & club FACUT
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                    .Where(i => i.Proba.numeProba == numeProba)
                                    //.Where(i => i.Sportiv.Club.nume == club)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ? repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == i.codUtilizator && ic.Club.nume == club)
                            .Select(ic => ic.Club.nume)
                            .FirstOrDefault() :
                            (i.Sportiv.Club.nume == club ? club : null),
                                        //numeClub = i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .Where(i => i.numeClub != null)
                                    .OrderBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;
            }
            else if (numeProba != "toate probele" && categorie != "toate categoriile" && club == "toate cluburile") //proba & categorie FACUT
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(i => i.codCompetitie == id)
                                    .Where(i => i.Proba.numeProba == numeProba)
                                    .Where(i => i.categorie == categorie)
                                    .Select(i => new IstoricRezultateCompetitieModel
                                    {
                                        numeSportiv = i.Sportiv.nume,
                                        prenumeSportiv = i.Sportiv.prenume,
                                        categorie = i.categorie,
                                        numeClub = i.Sportiv.dataInscreireClubActual > dataStart ?
                                           repoIstCl.GetIstoricCluburi()
                                            .Where(ic => ic.dataInscriereClub <= dataStart &&
                                                    ic.dataParasireClub >= dataFinal &&
                                                    ic.codUtilizator == i.codUtilizator)
                                            .Select(ic => ic.Club.nume)
                                            .FirstOrDefault() :
                                            i.Sportiv.Club.nume,
                                        //numeClub = i.Sportiv.Club.nume,
                                        numeProba = i.Proba.numeProba,
                                        timpTotal = i.timpTotal,
                                        locPesteToti = i.locPesteToti,
                                        locPerGen = i.locPerGen,
                                        locPerCatetogrie = i.locPerCategorie,
                                        puncte = i.puncte
                                    })
                                    .OrderBy(i => i.locPesteToti)
                                    .ToList();

                return istorice;
            }
            else if (numeProba == "toate probele" && categorie == "toate categoriile" && club != "toate cluburile")// club
            {
                var istorice = GetRezultateCompetitieClub(id, club); //rezolvat
                return istorice;
            }
            else if (numeProba == "toate probele" && categorie != "toate categoriile" && club == "toate cluburile")// categorie
            {
                var istorice = GetRezultateCompetitieCategorie(id, categorie); //rezolvat
                return istorice;
            }
            else if (numeProba != "toate probele" && categorie == "toate categoriile" && club == "toate cluburile")// categorie
            {
                var istorice = GetRezultateCompetitieNumeProba(id, numeProba); //rezolvat
                return istorice;
            }
            else//rezolvat
            {
                var istorice = repo.GetIstoricProbaCompetitieSportiv()
                                   .Where(i => i.codCompetitie == id)
                                   .Where(i => i.Proba.numeProba == numeProba)
                                   .Where(i => i.categorie == categorie)
                                   //.Where(i => i.Sportiv.Club.nume == club)
                                   .Select(i => new IstoricRezultateCompetitieModel
                                   {
                                       numeSportiv = i.Sportiv.nume,
                                       prenumeSportiv = i.Sportiv.prenume,
                                       categorie = i.categorie,
                                       numeClub = i.Sportiv.dataInscreireClubActual > dataStart ? repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == i.codUtilizator && ic.Club.nume == club)
                            .Select(ic => ic.Club.nume)
                            .FirstOrDefault() :
                            (i.Sportiv.Club.nume == club ? club : null),
                                       //numeClub = i.Sportiv.Club.nume,
                                       numeProba = i.Proba.numeProba,
                                       timpTotal = i.timpTotal,
                                       locPesteToti = i.locPesteToti,
                                       locPerGen = i.locPerGen,
                                       locPerCatetogrie = i.locPerCategorie,
                                       puncte = i.puncte
                                   })
                                   .Where(i => i.numeClub != null)
                                   .OrderBy(i => i.locPesteToti)
                                   .ToList();

                return istorice;
            }
        }


        public List<IerarhiePuncteModel> GetIerarhiePuncteCategClubAnProba(string categ = "toate categoriile", string club = "toate cluburile", string an = "toti anii",
         string proba = "toate probele")
        {
            int nrSportiviAfisati = 5;

            if (categ == "toate categoriile" && club == "toate cluburile" && an == "toti anii" && proba == "toate probele")
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                        /*numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic =>  ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(),
                        //  clubActual = g.FirstOrDefault(sp => sp.Sportiv.Id == g.Key).Sportiv.Club.nume,
                        clubActual =  g.FirstOrDefault().Sportiv.Club.nume,*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club == "toate cluburile" && an == "toti anii" && proba != "toate probele") //probele
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.Proba.numeProba == proba)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                       /* numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic => ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(),
                        clubActual = g.FirstOrDefault().Sportiv.Club.nume,*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club == "toate cluburile" && an != "toti anii" && proba == "toate probele") //an
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume,
                        /*numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(), 
                        //+ (g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) && g.FirstOrDefault().codUtilizator == g.Key ? $", {g.FirstOrDefault().Sportiv.nume}" : ""),
                        *//* .AsEnumerable()
                         .Concat(new[] {g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) && g.FirstOrDefault().codUtilizator == g.Key ? g.FirstOrDefault().Sportiv.nume : "" }.ToString())
                         .ToList()
                         .Aggregate((c1, c2) => c1 + ", " + c2),*//*
                        clubActual = g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) ? g.FirstOrDefault().Sportiv.Club.nume : "",*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club != "toate cluburile" && an == "toti anii" && proba == "toate probele") //club bifat
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()

                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                                                                          // .Where(x => x.Sportiv.Club.nume == club)
                    .GroupBy(x => x.Sportiv.Id)
                    /*.Where(predicate: g => repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.codUtilizator == g.Key && ic.Club.nume == club)
                            .ToList().Count() > 0)*/
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key
                        && ((sp.Sportiv.Club.nume == club && sp.Sportiv.dataInscreireClubActual <= sp.Competitie.dataStart) || (sp.Sportiv.IstoriceCluburi.Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= sp.Competitie.dataStart && i.dataParasireClub >= sp.Competitie.dataFinal).Count() > 0)
                           )
                         ).FirstOrDefault().Sportiv.nume,
                        /*prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,*/

                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key
                        && ((sp.Sportiv.Club.nume == club && sp.Sportiv.dataInscreireClubActual <= sp.Competitie.dataStart) || (sp.Sportiv.IstoriceCluburi.Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= sp.Competitie.dataStart && i.dataParasireClub >= sp.Competitie.dataFinal).Count() > 0)
                           )).FirstOrDefault().Sportiv.prenume,
                       /*             g.Where(sp => sp.Sportiv.Id == g.Key
                                    && (sp.Sportiv.Club.nume == club || sp.Sportiv.IstoriceCluburi.Where(sp => sp.codUtilizator == g.Key && sp.Club.nume == club).Count() > 0
                                        )
                                     ).FirstOrDefault().Sportiv.prenume,*/

                       // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                       /*                        numeClub = repoIstCl.GetIstoricCluburi()
                                                           .Where(ic =>  ic.codUtilizator == g.Key)
                                                           .Select(ic => ic.Club.nume)
                                                           .ToList(),
                                               clubActual = g.FirstOrDefault().Sportiv.Club.nume,*/
                       //trebuie sa luam punctele cand sportivul e la clubul dat
                       // puncte = g.Sum(x => x.puncte)
                       puncte = g.Where(sp => sp.Sportiv.Id == g.Key
                        && ((sp.Sportiv.Club.nume == club && sp.Sportiv.dataInscreireClubActual <= sp.Competitie.dataStart) || (sp.Sportiv.IstoriceCluburi.Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= sp.Competitie.dataStart && i.dataParasireClub >= sp.Competitie.dataFinal).Count() > 0)
                           )
                         ).Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv !=null && sp.prenumeSportiv != null)          
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club == "toate cluburile" && an == "toti anii" && proba == "toate probele") //categorie
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                        /*                        numeClub = repoIstCl.GetIstoricCluburi()
                                     .Where(ic =>  ic.codUtilizator == g.Key)
                                     .Select(ic => ic.Club.nume)
                                     .ToList(),
                         clubActual = g.FirstOrDefault().Sportiv.Club.nume,*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club == "toate cluburile" && an != "toti anii" && proba != "toate probele") //proba & an
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.Proba.numeProba == proba)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                       /* numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(), //+ (g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) && g.FirstOrDefault().codUtilizator == g.Key ? $", {g.FirstOrDefault().Sportiv.nume}" : ""),
                        *//* .AsEnumerable()
                         .Concat(new[] {g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) && g.FirstOrDefault().codUtilizator == g.Key ? g.FirstOrDefault().Sportiv.nume : "" }.ToString())
                         .ToList()
                         .Aggregate((c1, c2) => c1 + ", " + c2),*//*
                        clubActual = g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) ? g.FirstOrDefault().Sportiv.Club.nume : "",*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club != "toate cluburile" && an == "toti anii" && proba != "toate probele") //club & proba facut
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.Proba.numeProba == proba)
                    //.Where(x => x.Sportiv.Club.nume == club)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {

                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.prenume,
                        // numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        // prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                        /* numeClub = repoIstCl.GetIstoricCluburi()
                                     .Where(ic =>  ic.codUtilizator == g.Key)
                                     .Select(ic => ic.Club.nume)
                                     .ToList(),
                         clubActual = g.FirstOrDefault().Sportiv.Club.nume,*/
                        //puncte = g.Sum(x => x.puncte)
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart) 
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club != "toate cluburile" && an != "toti anii" && proba == "toate probele") //club & an facut
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                                                                          //  .Where(x => x.Sportiv.Club.nume == club)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                      && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                      || (x.Sportiv.IstoriceCluburi.
                      Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                         )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.prenume,
                        // numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        //prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                        /* numeClub = repoIstCl.GetIstoricCluburi()
                                     .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == g.Key)
                                     .Select(ic => ic.Club.nume)
                                     .ToList(),
                         clubActual = g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) ? g.FirstOrDefault().Sportiv.Club.nume : "",*/
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                       || (x.Sportiv.IstoriceCluburi.Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                          )
                         ).Sum(x => x.puncte)//g.Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ == "toate categoriile" && club != "toate cluburile" && an != "toti anii" && proba != "toate probele") //club & an & proba facut
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                                                                          // .Where(x => x.Sportiv.Club.nume == club)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .Where(x => x.Proba.numeProba == proba)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {

                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                        && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                        || (x.Sportiv.IstoriceCluburi.
                        Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                            )
                            ).FirstOrDefault().Sportiv.prenume,
                        //   numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        //prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //puncte = g.Sum(x => x.puncte)
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club == "toate cluburile" && an == "toti anii" && proba != "toate probele") // categorie & proba BIFAT
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                    .Where(x => x.Proba.numeProba == proba)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                        /*numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic =>  ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(),
                        clubActual =  g.FirstOrDefault().Sportiv.Club.nume,*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club == "toate cluburile" && an != "toti anii" && proba == "toate probele") // categorie & an BIFAT
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                       /* numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(),
                        clubActual = g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) ? g.FirstOrDefault().Sportiv.Club.nume : "",*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club == "toate cluburile" && an != "toti anii" && proba != "toate probele") //categorie & an & proba
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .Where(x => x.Proba.numeProba == proba)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // numeClub = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.Club.nume.ToList(),
                       /* numeClub = repoIstCl.GetIstoricCluburi()
                                    .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == g.Key)
                                    .Select(ic => ic.Club.nume)
                                    .ToList(),
                        clubActual = g.FirstOrDefault().Sportiv.dataInscreireClubActual.Year <= int.Parse(an) ? g.FirstOrDefault().Sportiv.Club.nume : "",*/
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club != "toate cluburile" && an == "toti anii" && proba == "toate probele") //categorie & club BIFAT
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                   // .Where(x => x.Sportiv.Club.nume == club)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        
                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                        && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                        || (x.Sportiv.IstoriceCluburi.
                        Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                            )
                            ).FirstOrDefault().Sportiv.prenume,
                        //   numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        //prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        //puncte = g.Sum(x => x.puncte)
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club != "toate cluburile" && an == "toti anii" && proba != "toate probele") //categorie & club & proba Facut
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                  //  .Where(x => x.Sportiv.Club.nume == club)
                    .Where(x => x.Proba.numeProba == proba)
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {
                        // numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                        //  prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                        // puncte = g.Sum(x => x.puncte)

                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                        && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                        || (x.Sportiv.IstoriceCluburi.
                        Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                            )
                            ).FirstOrDefault().Sportiv.prenume,
                       
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else if (categ != "toate categoriile" && club != "toate cluburile" && an != "toti anii" && proba == "toate probele") //categorie & club & an
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                    .Where(x => x.categorie == categ)
                   // .Where(x => x.Sportiv.Club.nume == club)
                    .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(x => x.Sportiv.Id)
                    .Select(g => new IerarhiePuncteModel
                    {

                        numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                          && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                          || (x.Sportiv.IstoriceCluburi.
                          Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                             )
                         ).FirstOrDefault().Sportiv.nume,

                        prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.prenume,
                      
                        puncte = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                       || (x.Sportiv.IstoriceCluburi.Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub.Year <= int.Parse(an) && i.dataParasireClub.Year >= int.Parse(an)).Count() > 0)
                          )
                         ).Sum(x => x.puncte)//g.Sum(x => x.puncte)
                    })
                    .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                    .OrderByDescending(x => x.puncte)
                    .Take(nrSportiviAfisati)
                    .ToList();
                return istoric;
            }

            else
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(x => x.Competitie.statusCompetitie == "activa")//doar pentru competitiile active
                         .Where(x => x.categorie == categ)
                      //   .Where(x => x.Sportiv.Club.nume == club)
                         .Where(x => x.Competitie.dataStart.Year == int.Parse(an))
                         .Where(x => x.Proba.numeProba == proba)
                         .GroupBy(x => x.Sportiv.Id)

                       .Select(g => new IerarhiePuncteModel
                       {
                           //   numeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.nume,
                           //  prenumeSportiv = g.Where(sp => sp.Sportiv.Id == g.Key).FirstOrDefault().Sportiv.prenume,
                           //  puncte = g.Sum(x => x.puncte)
                           numeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                       && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                       || (x.Sportiv.IstoriceCluburi.
                       Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                          )
                         ).FirstOrDefault().Sportiv.nume,

                           prenumeSportiv = g.Where(x => x.Sportiv.Id == g.Key
                           && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                           || (x.Sportiv.IstoriceCluburi.
                           Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                               )
                            ).FirstOrDefault().Sportiv.prenume,

                           puncte = g.Where(x => x.Sportiv.Id == g.Key
                          && ((x.Sportiv.Club.nume == club && x.Sportiv.dataInscreireClubActual <= x.Competitie.dataStart)
                          || (x.Sportiv.IstoriceCluburi.
                          Where(i => i.codUtilizator == g.Key && i.Club.nume == club && i.dataInscriereClub <= x.Competitie.dataStart && i.dataParasireClub >= x.Competitie.dataFinal).Count() > 0)
                             )
                         ).Sum(x => x.puncte)
                       })
                        .Where(sp => sp.numeSportiv != null && sp.prenumeSportiv != null)
                       
                       .OrderByDescending(x => x.puncte)
                       .Take(nrSportiviAfisati)
                       .ToList();
                return istoric;
            }

        }

         public List<IstoricCluburiTopCompetitieModel> GetTopCluburiByComp(int id)//id competitie
         {
            var competitie = repoComp.GetCompetitiiIQueryable()
                 .Where(c => c.codCompetitie == id)
                 .FirstOrDefault();
            var dataStart = competitie.dataStart;
            var dataFinal = competitie.dataFinal;

            int nrCluburiAfis = 5;

            //pentru cluburile actuale
             var istoric = repo.GetIstoricProbaCompetitieSportiv()
                 .Where(x => x.codCompetitie == id && x.Competitie.statusCompetitie == "activa")
                 .GroupBy(x => x.codUtilizator)//x.Sportiv.Club.nume
                 .Select(g => new IstoricCluburiTopCompetitieModelUtilizPuncte//IstoricCluburiTopCompetitieModel
                 {
                      puncte = g.Sum(x => x.puncte),
                      codSportiv = g.Key

                 })
                  //.Take(nrCluburiAfis)
                 .OrderByDescending(x => x.puncte)
                 .ToList();

            var utilizatoriPuncte = new List<CluburiSportiviPuncte>();

            for(int i=0; i < istoric.Count(); i++)
            {
                //pentru fiecare intrare vad care este clubul sportivului
                var sportivId = istoric[i].codSportiv;
                var sportiv = repo.GetSportivi()
                    .Where(s => s.Id == sportivId)
                    .FirstOrDefault();

                if(sportiv.dataInscreireClubActual <= dataStart)
                {
                    CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();

                    var numeClub = repoCl.GetCluburiIQueryable()
                        .Where(c => c.codClub == sportiv.codClub)
                        .Select(c => c.nume).FirstOrDefault();

                    clubPentruSportiv.numeClub = numeClub;
                    clubPentruSportiv.punctele = istoric[i].puncte;

                    utilizatoriPuncte.Add(clubPentruSportiv);

                   // istoric[i].numeClub = sportiv.Club.nume;
                }
                else
                {
                    var numeClub = repoIstCl.GetIstoricCluburi()
                       .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == sportivId)
                       .Select(ic => ic.Club.nume)
                       .FirstOrDefault();
                    //istoric[i].numeClub = numeClub;

                    CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();
                    clubPentruSportiv.numeClub = numeClub;
                    clubPentruSportiv.punctele = istoric[i].puncte;

                    utilizatoriPuncte.Add(clubPentruSportiv);
                }

            }

            //dupa acest for in locul sportivului va aparea numeleClubului
            //acum trebuie parcurs 
            var grupuri = utilizatoriPuncte.GroupBy(x => x.numeClub)
                               .Select(g => new IstoricCluburiTopCompetitieModel
                               {
                                   numeClub = g.Key,
                                   puncte = g.Sum(x => x.punctele)
                               })
                               .Take(nrCluburiAfis)
                               .OrderByDescending(x => x.puncte)
                               .ToList();
            return grupuri;
        }


        public List<IstoricCluburiTopCompetitieModel> GetTopCluburiPerAn(string an = "toti anii")
        {
            var nrCluburi = 5;
            if (an == "toti anii")
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(i => i.Competitie.statusCompetitie == "activa")
                    .GroupBy(i => new { i.codUtilizator, i.codCompetitie})
                    .Select(g => new
                    {
                        codSportiv = g.Key.codUtilizator,
                        codComp = g.Key.codCompetitie,
                        puncte = g.Sum(x => x.puncte)
                    })
                    .OrderByDescending(x => x.puncte)
                    .ToList();

                var utilizatoriCompPuncte = new List<CluburiSportiviPuncte>();

                for (int i = 0; i < istoric.Count(); i++)
                {
                    //pentru fiecare intrare vad care este clubul sportivului
                    var sportivId = istoric[i].codSportiv;
                    var competitieId = istoric[i].codComp;

                    var sportiv = repo.GetSportivi()
                        .Where(s => s.Id == sportivId)
                        .FirstOrDefault();

                    var competitie = repoComp.GetCompetitiiIQueryable()
                        .Where(c => c.codCompetitie == competitieId)
                        .FirstOrDefault();

                    var dataStart = competitie.dataStart;
                    var dataFinal = competitie.dataFinal;


                    if (sportiv.dataInscreireClubActual <= dataStart)
                    {
                        CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();

                        var numeClub = repoCl.GetCluburiIQueryable()
                            .Where(c => c.codClub == sportiv.codClub)
                            .Select(c => c.nume).FirstOrDefault();

                        clubPentruSportiv.numeClub = numeClub;
                        clubPentruSportiv.punctele = istoric[i].puncte;

                        utilizatoriCompPuncte.Add(clubPentruSportiv);
                    }
                    else
                    {
                        var numeClub = repoIstCl.GetIstoricCluburi()
                           .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == sportivId)
                           .Select(ic => ic.Club.nume)
                           .FirstOrDefault();

                        CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();
                        clubPentruSportiv.numeClub = numeClub;
                        clubPentruSportiv.punctele = istoric[i].puncte;

                        utilizatoriCompPuncte.Add(clubPentruSportiv);
                    }

                }

                //acum avem club si puncte

                var grupuri = utilizatoriCompPuncte.GroupBy(x => x.numeClub)
                               .Select(g => new IstoricCluburiTopCompetitieModel
                               {
                                   numeClub = g.Key,
                                   puncte = g.Sum(x => x.punctele)
                               })
                               .Take(nrCluburi)
                               .OrderByDescending(x => x.puncte)
                               .ToList();
                return grupuri;
            }

            else //nr an dat
            {

                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(i => i.Competitie.dataStart.Year == int.Parse(an))
                    .Where(i => i.Competitie.statusCompetitie == "activa")
                    .GroupBy(i => new { i.codUtilizator, i.codCompetitie })
                    .Select(g => new
                    {
                        codSportiv = g.Key.codUtilizator,
                        codComp = g.Key.codCompetitie,
                        puncte = g.Sum(x => x.puncte)
                    })
                    /*.GroupBy(i => i.codUtilizator)
                    .Select(g => new
                    {
                        codSportiv = g.Key,
                        puncte = g.Sum(x => x.puncte)
                    })*/
                    .OrderByDescending(x => x.puncte)
                    .ToList();

                //in istoric o sa avem doar comp din anul dat
                if(istoric.Count() == 0)
                {
                    return new List<IstoricCluburiTopCompetitieModel>();
                }

                var utilizatoriCompPuncte = new List<CluburiSportiviPuncte>();

                for (int i = 0; i < istoric.Count(); i++)
                {
                    //pentru fiecare intrare vad care este clubul sportivului
                    var sportivId = istoric[i].codSportiv;
                    var competitieId = istoric[i].codComp;

                    var sportiv = repo.GetSportivi()
                        .Where(s => s.Id == sportivId)
                        .FirstOrDefault();

                    var competitie = repoComp.GetCompetitiiIQueryable()
                        .Where(c => c.codCompetitie == competitieId)
                        .FirstOrDefault();

                    var dataStart = competitie.dataStart;
                    var dataFinal = competitie.dataFinal;


                    if (sportiv.dataInscreireClubActual <= dataStart)
                    {
                        CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();

                        var numeClub = repoCl.GetCluburiIQueryable()
                            .Where(c => c.codClub == sportiv.codClub)
                            .Select(c => c.nume).FirstOrDefault();

                        clubPentruSportiv.numeClub = numeClub;
                        clubPentruSportiv.punctele = istoric[i].puncte;

                        utilizatoriCompPuncte.Add(clubPentruSportiv);
                    }
                    else
                    {
                        var numeClub = repoIstCl.GetIstoricCluburi()
                           .Where(ic => ic.dataInscriereClub <= dataStart && ic.dataParasireClub >= dataFinal && ic.codUtilizator == sportivId)
                           .Select(ic => ic.Club.nume)
                           .FirstOrDefault();

                        CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();
                        clubPentruSportiv.numeClub = numeClub;
                        clubPentruSportiv.punctele = istoric[i].puncte;

                        utilizatoriCompPuncte.Add(clubPentruSportiv);
                    }
                    /* if (sportiv.dataInscreireClubActual.Year <= int.Parse(an))
                     {
                         CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();

                         var numeClub = repoCl.GetCluburiIQueryable()
                             .Where(c => c.codClub == sportiv.codClub)
                             .Select(c => c.nume).FirstOrDefault();

                         clubPentruSportiv.numeClub = numeClub;
                         clubPentruSportiv.punctele = istoric[i].puncte;

                         utilizatoriCompPuncte.Add(clubPentruSportiv);
                     }
                     else
                     {
                         var numeClub = repoIstCl.GetIstoricCluburi()
                            .Where(ic => ic.dataInscriereClub.Year <= int.Parse(an) && ic.dataParasireClub.Year >= int.Parse(an) && ic.codUtilizator == sportivId)
                            .Select(ic => ic.Club.nume)
                            .FirstOrDefault();

                         CluburiSportiviPuncte clubPentruSportiv = new CluburiSportiviPuncte();
                         clubPentruSportiv.numeClub = numeClub;
                         clubPentruSportiv.punctele = istoric[i].puncte;

                         utilizatoriCompPuncte.Add(clubPentruSportiv);
                     }*/

                }

                //acum avem club si puncte

                var grupuri = utilizatoriCompPuncte.GroupBy(x => x.numeClub)
                               .Select(g => new IstoricCluburiTopCompetitieModel
                               {
                                   numeClub = g.Key,
                                   puncte = g.Sum(x => x.punctele)
                               })
                               .Take(nrCluburi)
                               .OrderByDescending(x => x.puncte)
                               .ToList();
                return grupuri;
                /*var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(i => i.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(i => i.Sportiv.Club.nume)
                    .Select(i => new IstoricCluburiTopCompetitieModel
                    {
                        puncte = i.Sum(x => x.puncte),
                        numeClub = i.Key
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrCluburi)
                    .ToList();

                return istoric;*/
            }

        }
      /*  public List<IstoricCluburiTopCompetitieModel> GetTopCluburiPerAn(string an = "toti anii")
        {
            var nrCluburi = 5;
            if (an == "toti anii")
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .GroupBy(i => i.Sportiv.Club.nume)
                    .Select(i => new IstoricCluburiTopCompetitieModel
                    {
                        puncte = i.Sum(x => x.puncte),
                        numeClub = i.Key
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrCluburi)
                    .ToList();

                return istoric;
            }
            else
            {
                var istoric = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(i => i.Competitie.dataStart.Year == int.Parse(an))
                    .GroupBy(i => i.Sportiv.Club.nume)
                    .Select(i => new IstoricCluburiTopCompetitieModel
                    {
                        puncte = i.Sum(x => x.puncte),
                        numeClub = i.Key
                    })
                    .OrderByDescending(x => x.puncte)
                    .Take(nrCluburi)
                    .ToList();

                return istoric;
            }

        }*/

        public List<int> GetStatisticaMedaliiSpAntrComp(string emailAntrenor, string numeCompetitie)
        {
            var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);
            if (antrenor == null)
            {
                return new List<int>();
            }
            var idAntrenor = antrenor.Result.Id;
            List<int> result = new List<int>();

            if (numeCompetitie != "toate competițiile")

            {
                int nrMedaliiiLoc1 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                    .Where(s => s.locPerCategorie == 1)
                    .Count();

                int nrMedaliiLoc2 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                    .Where(s => s.locPerCategorie == 2)
                    .Count();

                int nrMedaliiLoc3 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                    .Where(s => s.locPerCategorie == 3)
                    .Count();

                result.Add(nrMedaliiiLoc1);
                result.Add(nrMedaliiLoc2);
                result.Add(nrMedaliiLoc3);
            }
            else
            {
                int nrMedaliiiLoc1 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.locPerCategorie == 1)
                    .Count();

                int nrMedaliiLoc2 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.locPerCategorie == 2)
                    .Count();

                int nrMedaliiLoc3 = repo.GetIstoricProbaCompetitieSportiv()
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Where(s => s.locPerCategorie == 3)
                    .Count();


                result.Add(nrMedaliiiLoc1);
                result.Add(nrMedaliiLoc2);
                result.Add(nrMedaliiLoc3);
            }

            return result;

        }

        /* public List<int> GetStatisticaMedaliiSpAntrComp(string idAntrenor, string numeCompetitie)
         {
             List<int> result = new List<int>();

             int nrMedaliiiLoc1 = repo.GetIstoricProbaCompetitieSportiv()
                 .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                 .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                 .Where(s => s.locPerCategorie == 1)
                 .Count();

             int nrMedaliiLoc2 = repo.GetIstoricProbaCompetitieSportiv()
                 .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                 .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                 .Where(s => s.locPerCategorie == 2)
                 .Count();

             int nrMedaliiLoc3 = repo.GetIstoricProbaCompetitieSportiv()
                 .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                 .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                 .Where(s => s.locPerCategorie == 3)
                 .Count();

             result.Add(nrMedaliiiLoc1);
             result.Add(nrMedaliiLoc2);
             result.Add(nrMedaliiLoc3);

             return result;

         }*/
        public List<double> GetStatisticaPodiumSpAntrComp(string emailAntrenor, string numeCompetitie)
        {//la inscriere automat se pune locul 999 - daca sportivul are locul 999 la cetogorie nu inseamna ca e nemedaliat
            var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);
            if(antrenor == null)
            {
                return new List<double>();
            }
            var idAntrenor = antrenor.Result.Id;
            List<double> result = new List<double>();
            List<string> spMedaliati = new List<string>();
            int nrSportiviNemedaliati = 0;
            double procentajMedaliati = 0;
            double procentajNemedaliati = 0;

            if (numeCompetitie != "toate competițiile")
            {
                spMedaliati = repo.GetIstoricProbaCompetitieSportiv()
                .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                .GroupBy(s => s.codUtilizator)
                .Select(s => s.Key)
                .ToList();

                //trebuie verificat pentru sportivii antrenorului care sunt cei care au doar intrari cu loc = 999
                /*                spDoarCuInscrieri = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                    .Where(s => s.Competitie.numeCompetitie== numeCompetitie)
                */
                var spDoarCuInscrieri = repo.GetIstoricProbaCompetitieSportiv()
                    .Include(s => s.Sportiv)
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Include(s => s.Competitie)
                    .Where(s => s.locPerCategorie == 999 && s.Competitie.numeCompetitie == numeCompetitie)
                    .Where(s => !repo.GetIstoricProbaCompetitieSportiv().Any(y => y.codUtilizator == s.Sportiv.Id && y.locPerCategorie != 999 && y.Competitie.numeCompetitie == numeCompetitie))
                    .Select(s => s.Sportiv.Id)
                    .Distinct()
                    .ToList();


                //nrSportivi se refera la cei care numarul sportivilor care au concurat
                int nrSportivi = repo.GetIstoricProbaCompetitieSportiv()
                                .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                                .GroupBy(s => s.codUtilizator)
                                .Count();

                nrSportivi -= spDoarCuInscrieri.Count();


                int nrSportiviMedaliati = spMedaliati.Count();/* repo.GetIstoricProbaCompetitieSportiv()
                                .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                                .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                                .GroupBy(s => s.codUtilizator)
                                .Count();*/

                //sportivii care sunt doar inscrisi nu sunt nemedaliati
                nrSportiviNemedaliati = repo.GetIstoricProbaCompetitieSportiv()
               .Where(s => s.Sportiv.codAntrenor == idAntrenor)
               .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
               .Where(s => s.locPerCategorie > 3 && !spMedaliati.Contains(s.codUtilizator) && !spDoarCuInscrieri.Contains(s.codUtilizator))// se verifica daca sportivul este printre cei medaliati (daca un sportiv nu ia podium la o proba, dar la alta a luat atunci el e considerat medaliat)
               .GroupBy(s => s.codUtilizator)
               .Count();

                if (nrSportivi != 0)
                {
                    procentajMedaliati = Math.Round(((double)nrSportiviMedaliati / nrSportivi) * 100, 2);
                    procentajNemedaliati = Math.Round(((double)nrSportiviNemedaliati / nrSportivi) * 100, 2);
                }

                result.Add(procentajMedaliati);
                result.Add(procentajNemedaliati);
            }
            else
            {
                spMedaliati = repo.GetIstoricProbaCompetitieSportiv()
                 .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                 .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                 .GroupBy(s => s.codUtilizator)
                 .Select(s => s.Key)
                 .ToList();

                //trebuie verificat pentru sportivii antrenorului care sunt cei care au doar intrari cu loc = 999
                /*                spDoarCuInscrieri = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                    .Where(s => s.Competitie.numeCompetitie== numeCompetitie)
                */
                var spDoarCuInscrieri = repo.GetIstoricProbaCompetitieSportiv()
                    .Include(s => s.Sportiv)
                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                    .Include(s => s.Competitie)
                    .Where(s => s.locPerCategorie == 999)
                    .Where(s => !repo.GetIstoricProbaCompetitieSportiv().Any(y => y.codUtilizator == s.Sportiv.Id && y.locPerCategorie != 999))
                    .Select(s => s.Sportiv.Id)
                    .Distinct()
                    .ToList();


                //nrSportivi se refera la cei care numarul sportivilor care au concurat
                int nrSportivi = repo.GetIstoricProbaCompetitieSportiv()
                                .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                .GroupBy(s => s.codUtilizator)
                                .Count();

                nrSportivi -= spDoarCuInscrieri.Count();


                int nrSportiviMedaliati = spMedaliati.Count();/* repo.GetIstoricProbaCompetitieSportiv()
                                .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                                .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                                .GroupBy(s => s.codUtilizator)
                                .Count();*/

                //sportivii care sunt doar inscrisi nu sunt nemedaliati
                nrSportiviNemedaliati = repo.GetIstoricProbaCompetitieSportiv()
               .Where(s => s.Sportiv.codAntrenor == idAntrenor)
               .Where(s => s.locPerCategorie > 3 && !spMedaliati.Contains(s.codUtilizator) && !spDoarCuInscrieri.Contains(s.codUtilizator))// se verifica daca sportivul este printre cei medaliati (daca un sportiv nu ia podium la o proba, dar la alta a luat atunci el e considerat medaliat)
               .GroupBy(s => s.codUtilizator)
               .Count();

                if (nrSportivi != 0)
                {
                    procentajMedaliati = Math.Round(((double)nrSportiviMedaliati / nrSportivi) * 100, 2);
                    procentajNemedaliati = Math.Round(((double)nrSportiviNemedaliati / nrSportivi) * 100, 2);
                }

                result.Add(procentajMedaliati);
                result.Add(procentajNemedaliati);
            }
            return result;
        }

        /*        public List<double> GetStatisticaPodiumSpAntrComp(string idAntrenor, string numeCompetitie)
                {
                    List<double> result = new List<double>();
                    List<string> spMedaliati = new List<string>();
                    int nrSportiviNemedaliati = 0;
                    double procentajMedaliati = 0;
                    double procentajNemedaliati = 0;
                    spMedaliati = repo.GetIstoricProbaCompetitieSportiv()
                        .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                        .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                        .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                        .GroupBy(s => s.codUtilizator)
                        .Select(s => s.Key)
                        .ToList();


                    int nrSportivi = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                    .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                                    .GroupBy(s => s.codUtilizator)
                                    .Count();
                    int nrSportiviMedaliati = repo.GetIstoricProbaCompetitieSportiv()
                                    .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                                    .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                                    .Where(s => s.locPerCategorie == 1 || s.locPerCategorie == 2 || s.locPerCategorie == 3)
                                    .GroupBy(s => s.codUtilizator)
                                    .Count();

                    foreach (var sp in spMedaliati)
                    {
                         nrSportiviNemedaliati = repo.GetIstoricProbaCompetitieSportiv()
                        .Where(s => s.Sportiv.codAntrenor == idAntrenor)
                        .Where(s => s.Competitie.numeCompetitie == numeCompetitie)
                        .Where(s => s.locPerCategorie > 3 && s.codUtilizator != sp)
                        .GroupBy(s => s.codUtilizator)
                        .Count();
                    }
                    if (nrSportivi != 0)
                    {
                        procentajMedaliati = Math.Round(((double)nrSportiviMedaliati / nrSportivi) * 100, 2);
                        procentajNemedaliati = Math.Round(((double)nrSportiviNemedaliati / nrSportivi) * 100, 2);
                    }

                    result.Add(procentajMedaliati);
                    result.Add(procentajNemedaliati);

                    return result;
                }*/
    }
}
