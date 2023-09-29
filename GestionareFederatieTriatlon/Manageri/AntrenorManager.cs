using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Manageri
{
    public class AntrenorManager: IAntrenorManager
    {
        private readonly IAntrenorRepo repo;
        private readonly ISportivRepo repoSp;
        private readonly IIstoricClubRepo repoIstCl;
        private readonly UserManager<Utilizator> utilizatorManager;
        public AntrenorManager(IAntrenorRepo repo,ISportivRepo repoSp, IIstoricClubRepo repoIstCl, UserManager<Utilizator> utilizatorManager)
        {
            this.repo = repo;
            this.repoSp = repoSp;
            this.repoIstCl = repoIstCl;
            this.utilizatorManager= utilizatorManager;
        }
        public static string Abonarea(bool abonat)
        {
            if (abonat == false)
                return "nu este abonat la stiri";
            else
                return "este abonat la stiri";

        }
        public List<AntrenoriModel> GetAntrenori()
        {
            var antrenori = repo.GetAntrenorIQueryable()
                .Select(a => new AntrenoriModel
                {
                    nume= a.nume,
                    prenume= a.prenume, 
                    urlPozaProfil= a.urlPozaProfil,
                    numeClub = a.Club.nume
                })
                .OrderBy(a => a.numeClub) 
                .ToList();
            return antrenori;
        }

        public List<AntrenoriClubModel> GetAntrenoriByClubId(int id)
        {
            var antrenori = repo.GetAntrenorIQueryable()
                .Where(a => a.Club.codClub == id)
                .Select(a => new AntrenoriClubModel
                {
                    nume= a.nume,
                    prenume= a.prenume,
                    gradPregatire= a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                })
                .OrderBy(a => a.nume)
                .ToList();
            return antrenori;
        }

        public List<AntrenoriClubMailModel> GetAntrenoriSearchByClubId(int id, string numeFam, string prenume)
        {
            var antrenori = new List<AntrenoriClubMailModel>();
            if (numeFam == "null" && prenume == "null")
            {
                antrenori = repo.GetAntrenorIQueryable()
                .Where(a => a.Club.codClub == id)
                .Select(a => new AntrenoriClubMailModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                    email = a.Email
                })
                .OrderBy(a => a.nume)
                .ToList();
                return antrenori;
            }
            else if (numeFam != "null" && prenume == "null")
            {
                antrenori = repo.GetAntrenorIQueryable()
                .Where(a => a.Club.codClub == id && a.nume.Contains(numeFam))
                .Select(a => new AntrenoriClubMailModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                    email = a.Email
                })
                .OrderBy(a => a.nume)
                .ToList();
                return antrenori;
            }

            else if (numeFam == "null" && prenume != "null")
            {
                antrenori = repo.GetAntrenorIQueryable()
                .Where(a => a.Club.codClub == id && a.prenume.Contains(prenume))
                .Select(a => new AntrenoriClubMailModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                    email = a.Email
                })
                .OrderBy(a => a.nume)
                .ToList();
                return antrenori;
            }
            else
            {
                antrenori = repo.GetAntrenorIQueryable()
               .Where(a => a.Club.codClub == id && a.nume.Contains(numeFam) && a.prenume.Contains(prenume))
               .Select(a => new AntrenoriClubMailModel
               {
                   nume = a.nume,
                   prenume = a.prenume,
                   gradPregatire = a.gradPregatire,
                   urlPozaProfil = a.urlPozaProfil,
                   email = a.Email
               })
               .OrderBy(a => a.nume)
               .ToList();
                return antrenori;
            }


        }
        /*  public List<AntrenoriClubModel> GetAntrenoriSearchByClubId(int id,string numeFam,string prenume)
          {
              var antrenori = new List<AntrenoriClubModel>();
              if(numeFam== "null" && prenume == "null")
              {
                  antrenori = GetAntrenoriByClubId(id);
                  return antrenori;
              }
              else if(numeFam != "null" && prenume == "null")
              {
                  antrenori = repo.GetAntrenorIQueryable()
                  .Where(a => a.Club.codClub == id && a.nume.Contains(numeFam))
                  .Select(a => new AntrenoriClubModel
                  {
                      nume = a.nume,
                      prenume = a.prenume,
                      gradPregatire = a.gradPregatire,
                      urlPozaProfil = a.urlPozaProfil,
                  })
                  .OrderBy(a => a.nume)
                  .ToList();
                  return antrenori;
              }

              else if (numeFam == "null" && prenume != "null")
              {
                  antrenori = repo.GetAntrenorIQueryable()
                  .Where(a => a.Club.codClub == id && a.prenume.Contains(prenume))
                  .Select(a => new AntrenoriClubModel
                  {
                      nume = a.nume,
                      prenume = a.prenume,
                      gradPregatire = a.gradPregatire,
                      urlPozaProfil = a.urlPozaProfil,
                  })
                  .OrderBy(a => a.nume)
                  .ToList();
                  return antrenori;
              }
              else 
              {
                 antrenori = repo.GetAntrenorIQueryable()
                .Where(a => a.Club.codClub == id && a.nume.Contains(numeFam) && a.prenume.Contains(prenume))
                .Select(a => new AntrenoriClubModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                })
                .OrderBy(a => a.nume)
                .ToList();
                  return antrenori;
              }


          }*/

        /*        public AntrenorByIdModel GetAntrenorInfo(string id)
                {
                    var antrenor = repo.GetAntrenorIQueryable()
                        .Where(a => a.Id == id)
                        .Select(a => new AntrenorByIdModel
                        {
                            nume = a.nume,
                            prenume = a.prenume,
                            gradPregatire = a.gradPregatire,
                            abonareStiri = Abonarea(a.abonareStiri),
                            urlPozaProfil = a.urlPozaProfil,
                            numeClub = a.Club.nume,
                            email = a.Email

                        })
                        .FirstOrDefault();

                    return antrenor;
                }*/

        public AntrenorByIdModel GetAntrenorInfo(string emailAntrenor)
        {
            var antrenor = repo.GetAntrenorIQueryable()
                .Where(a => a.Email == emailAntrenor)
                .Select(a => new AntrenorByIdModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    abonareStiri = Abonarea(a.abonareStiri),
                    urlPozaProfil = a.urlPozaProfil,
                    numeClub = a.Club.nume,
                    email = a.Email

                })
                .FirstOrDefault();

            return antrenor;
        }

        /*        public AntrenorByIdViewSpModel GetAntrenorInfoBySp(string id)
                {
                    var antrenor = repo.GetAntrenorIQueryable()
                        .Where(a => a.Id == id)
                        .Select(a => new AntrenorByIdViewSpModel
                        {
                            nume = a.nume,
                            prenume = a.prenume,
                            gradPregatire = a.gradPregatire,
                            urlPozaProfil = a.urlPozaProfil,
                            numeClub = a.Club.nume,
                            email = a.Email
                        })
                        .FirstOrDefault();

                    return antrenor;
                }*/
        public AntrenorByIdViewSpModel GetAntrenorInfoBySp(string email)
        {
            var antrenor = repo.GetAntrenorIQueryable()
                .Where(a => a.Email == email)
                .Select(a => new AntrenorByIdViewSpModel
                {
                    nume = a.nume,
                    prenume = a.prenume,
                    gradPregatire = a.gradPregatire,
                    urlPozaProfil = a.urlPozaProfil,
                    numeClub = a.Club.nume,
                    email = a.Email
                })
                .FirstOrDefault();

            return antrenor;
        }
        public int GetNumarSportiviActualiAntr(string email)
        {
            var antrenor = repo.GetAntrenorIQueryable().FirstOrDefault(a => a.Email == email);
            var numarSportiviAntrenorClubActual = repoSp.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Id)
                .Count();
            return numarSportiviAntrenorClubActual;
        }
        public void Update(AntrenorUpdateModel model)
        {
            var antrenorul = utilizatorManager.FindByEmailAsync(model.emailAntrenor).Result;

            var antrenor = repo.GetAntrenorIQueryable().FirstOrDefault(a => a.Email == model.emailAntrenor);
            var club = repo.GetClubIQueryable().FirstOrDefault(c => c.nume == model.numeClub);

            //daca antrenorul alege sa isi modifice clubul atunci acest lucru e posibil doar daca nu mai are sportivi la clubul curent

            var numarSportiviAntrenorClubActual = repoSp.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Id)
                .Count();


            if (antrenor == null || club == null|| antrenorul == null)
                return;

            if(antrenorul.codClub != club.codClub)//daca se muta la alt club
            {
                if(numarSportiviAntrenorClubActual > 0)//daca inca mai are sportivi nu se poate muta
                {
                    return;
                }
                //daca se muta la alt club atunci se adauga club vechi in creareIst
                var istoricClub = new IstoricClub
                {
                    /*codClub = antrenor.Club.codClub,*/
                    codClub = antrenorul.codClub,
                    codUtilizator = antrenor.Id,
                    dataInscriereClub = antrenor.dataInscreireClubActual,
                    dataParasireClub = DateTime.Today.AddDays(-1)
                };
                repoIstCl.Create(istoricClub);

                //acum se poate face update
               // antrenor.Id = model.Id;
                antrenor.nume = model.nume;
                antrenor.prenume = model.prenume;
                antrenor.gradPregatire = model.gradPregatire;
                antrenor.urlPozaProfil = model.urlPozaProfil;
                antrenor.abonareStiri = model.abonareStiri;
                antrenor.codClub = club.codClub;

                antrenor.dataInscreireClubActual = DateTime.Now;

                repo.Update(antrenor);

            }
            else
            {
               // antrenor.Id = model.;
                antrenor.nume = model.nume;
                antrenor.prenume = model.prenume;
                antrenor.gradPregatire = model.gradPregatire;
                antrenor.urlPozaProfil = model.urlPozaProfil;
                antrenor.abonareStiri = model.abonareStiri;

                // antrenor.codClub = model.codClub;
                antrenor.codClub = club.codClub;

                repo.Update(antrenor);
            }

           
        }
    }
}
