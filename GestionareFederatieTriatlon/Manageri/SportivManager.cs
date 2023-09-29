using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GestionareFederatieTriatlon.Manageri
{
    public class SportivManager: ISportivManager
    {
        private readonly ISportivRepo repo;
        private readonly IIstoricClubRepo repoIstCl;
        private readonly UserManager<Utilizator> utilizatorManager;
        public SportivManager(ISportivRepo repo, UserManager<Utilizator> utilizatorManager,IIstoricClubRepo repoIstCl)
        {
            this.repo = repo;
            this.utilizatorManager = utilizatorManager;
            this.repoIstCl = repoIstCl;
        }
        public static string Abonarea(bool abonat)
        {
            if (abonat == false)
                return "nu este abonat la stiri";
            else
                return "este abonat la stiri";
        }


        /*        public SportivByIdModel GetSportivById(string id)//repo.GetClubIQueryable().FirstOrDefault(c => c.nume == model.numeClub);
                {
                    var sportiv = repo.GetSportiviIQueryable()
                        .Where(s => s.Id == id)
                        .Select(s => new SportivByIdModel
                        {
                            urlPozaProfil = s.urlPozaProfil,
                            numarLegitimatie = s.numarLegitimatie,
                            nume = s.nume,
                            prenume = s.prenume,
                            gen = s.gen,
                            anNastere = s.dataNastere.Year,
                            abonareStiri = Abonarea(s.abonareStiri),
                            numeClub = s.Club.nume,
                            numeAntrenor = s.AntrenorUtilizator.nume,//repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Id == s.codAntrenor).nume,
                            prenumeAntrenor = s.AntrenorUtilizator.prenume//repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Id == s.codAntrenor).prenume

                        }).FirstOrDefault();
                    return sportiv;
                }*/
        public SportivByIdModel GetSportivById(string email)//repo.GetClubIQueryable().FirstOrDefault(c => c.nume == model.numeClub);
        {
            var sportiv = repo.GetSportiviIQueryable()
                .Where(s => s.Email == email)
                .Select(s => new SportivByIdModel
                {
                    urlPozaProfil = s.urlPozaProfil,
                    numarLegitimatie = s.numarLegitimatie,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year,
                    abonareStiri = Abonarea(s.abonareStiri),
                    numeClub = s.Club.nume,
                    emailAntrenor = s.AntrenorUtilizator.Email,
                    numeAntrenor = s.AntrenorUtilizator.nume,//repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Id == s.codAntrenor).nume,
                    prenumeAntrenor = s.AntrenorUtilizator.prenume//repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Id == s.codAntrenor).prenume

                }).FirstOrDefault();
            return sportiv;
        }

        /*        public SportivByIdModelOtherView GetSportivByIdView(string id)//repo.GetClubIQueryable().FirstOrDefault(c => c.nume == model.numeClub);
                {
                    var sportiv = repo.GetSportiviIQueryable()
                        .Where(s => s.Id == id)
                        .Select(s => new SportivByIdModelOtherView
                        {
                            urlPozaProfil = s.urlPozaProfil,
                            numarLegitimatie = s.numarLegitimatie,
                            nume = s.nume,
                            prenume = s.prenume,
                            gen = s.gen,
                            anNastere = s.dataNastere.Year,
                            numeClub = s.Club.nume

                        }).FirstOrDefault();
                    return sportiv;
                }*/
        public int GetVarstaSportivLeg(int legitimatie)
        {
            var sportivVarsta = repo.GetSportiviIQueryable()
                .Where(s => s.numarLegitimatie == legitimatie)
                .Select(s => s.dataNastere.Year)
                .FirstOrDefault();
            return sportivVarsta;
        }

        public GenSportiv GetGenSportivLeg(int legitimatie)
        {
            var sportivGen = repo.GetSportiviIQueryable()
                .Where(s => s.numarLegitimatie == legitimatie)
                .Select(s => new GenSportiv
                {
                    gen = s.gen
                })
                .FirstOrDefault();
           return sportivGen;
        }

        public EmailSportiv GetEmailSportivLegitimat(int legitimatie)
        {
            var sportiv = repo.GetSportiviIQueryable()
                .Where(s => s.numarLegitimatie == legitimatie)
                .Select(s => new EmailSportiv
                {
                    emailSportiv = s.Email,
                    nume = s.nume,
                    prenume = s.prenume,
                    abonareStiri = s.abonareStiri
                    
                })
                .FirstOrDefault();
            return sportiv;
        }

        public SportivByIdModelOtherView GetSportivByEmailView(string email)//repo.GetClubIQueryable().FirstOrDefault(c => c.nume == model.numeClub);
        {
            var sportiv = repo.GetSportiviIQueryable()
                .Where(s => s.Email == email)
                .Select(s => new SportivByIdModelOtherView
                {
                    urlPozaProfil = s.urlPozaProfil,
                    numarLegitimatie = s.numarLegitimatie,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year,
                    numeClub = s.Club.nume

                }).FirstOrDefault();
            return sportiv;
        }

        /*        public List<SportiviAntrenorModel> GetSportiviForAntrenorById(string id)
                {
                    var sportivi = repo.GetSportiviIQueryable()
                        .Where(s => s.codAntrenor == id)
                        .Select(s => new SportiviAntrenorModel
                        {
                            numarLegitimatie = s.numarLegitimatie,
                            urlPozaProfil = s.urlPozaProfil,
                            nume = s.nume,
                            prenume = s.prenume,
                            gen = s.gen,
                            anNastere = s.dataNastere.Year
                        })
                        .OrderBy(s => s.numarLegitimatie)
                        .ToList();
                    return sportivi;
                }*/


        public List<SportiviAntrenorModel> GetSportiviForAntrenorByEmail(string emailAntrenor)
        {
            //trebuie gasit antrenorul cu email dat
            var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);


            var sportivi = repo.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Result.Id)
                .Select(s => new SportiviAntrenorModel
                {
                    email = s.Email,
                    numarLegitimatie = s.numarLegitimatie,
                    urlPozaProfil = s.urlPozaProfil,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year
                })
                .OrderBy(s => s.numarLegitimatie)
                .ToList();
            return sportivi;
        }


        public List<SportiviAntrenorModel> GetSportiviFilterForAntrenorByEmail(string emailAntrenor, string gen = "toate genurile",string anNastere = "toti anii")
        {
            if(gen == "toate genurile" && anNastere == "toti anii")
            {
                var sportivi = GetSportiviForAntrenorByEmail(emailAntrenor);
                return sportivi;
            }
            else if(gen != "toate genurile" && anNastere == "toti anii")//gen ales
            {
                var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);
                var sportivi = repo.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Result.Id)
                .Where(s => s.gen == gen)
                .Select(s => new SportiviAntrenorModel
                {
                    email = s.Email,
                    numarLegitimatie = s.numarLegitimatie,
                    urlPozaProfil = s.urlPozaProfil,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year
                })
                .OrderBy(s => s.numarLegitimatie)
                .ToList();
                return sportivi;
            }
            else if (gen == "toate genurile" && anNastere != "toti anii")//anNastere ales
            {
                var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);
                var sportivi = repo.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Result.Id)
                .Where(s => s.dataNastere.Year == Convert.ToInt32(anNastere))
                .Select(s => new SportiviAntrenorModel
                {
                    email = s.Email,
                    numarLegitimatie = s.numarLegitimatie,
                    urlPozaProfil = s.urlPozaProfil,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year
                })
                .OrderBy(s => s.numarLegitimatie)
                .ToList();
                return sportivi;
            }
            else
            {
                var antrenor = utilizatorManager.FindByEmailAsync(emailAntrenor);
                var sportivi = repo.GetSportiviIQueryable()
                .Where(s => s.codAntrenor == antrenor.Result.Id)
                .Where(s => s.gen == gen)
                .Where(s => s.dataNastere.Year == Convert.ToInt32(anNastere))
                .Select(s => new SportiviAntrenorModel
                {
                    email = s.Email,
                    numarLegitimatie = s.numarLegitimatie,
                    urlPozaProfil = s.urlPozaProfil,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year
                })
                .OrderBy(s => s.numarLegitimatie)
                .ToList();
                return sportivi;
            }
        }


        public List<SportiviAntrenorModel> GetSportiviClubById(int id)
        {
            var sportivi = repo.GetSportiviIQueryable()
                .Where(s => s.codClub == id)
                .Select(s => new SportiviAntrenorModel
                {
                    numarLegitimatie = s.numarLegitimatie,
                    urlPozaProfil = s.urlPozaProfil,
                    nume = s.nume,
                    prenume = s.prenume,
                    gen = s.gen,
                    anNastere = s.dataNastere.Year,
                    email  = s.Email
                })
                .OrderBy(s => s.numarLegitimatie)
                .ToList();
            return sportivi;
        }

        public List<SportiviAntrenorModel> GetSportiviClubByIdSearch(int id, string numeFam,string prenume)
        {
            var sportivi = new List<SportiviAntrenorModel>();

            if (numeFam == "null" && prenume == "null")
            {
                sportivi = GetSportiviClubById(id);
                return sportivi;
            }
            else if (numeFam != "null" && prenume == "null")
            {
                sportivi = repo.GetSportiviIQueryable()
                    .Where(s => s.codClub == id && s.nume.Contains(numeFam))
                    .Select(s => new SportiviAntrenorModel
                    {
                        numarLegitimatie = s.numarLegitimatie,
                        urlPozaProfil = s.urlPozaProfil,
                        nume = s.nume,
                        prenume = s.prenume,
                        gen = s.gen,
                        anNastere = s.dataNastere.Year,
                        email = s.Email
                    })
                    .OrderBy(s => s.numarLegitimatie)
                    .ToList();
                return sportivi;
            }
            else if (numeFam == "null" && prenume != "null")
            {
                sportivi = repo.GetSportiviIQueryable()
                       .Where(s => s.codClub == id && s.prenume.Contains(prenume))
                       .Select(s => new SportiviAntrenorModel
                       {
                           numarLegitimatie = s.numarLegitimatie,
                           urlPozaProfil = s.urlPozaProfil,
                           nume = s.nume,
                           prenume = s.prenume,
                           gen = s.gen,
                           anNastere = s.dataNastere.Year,
                           email = s.Email
                       })
                       .OrderBy(s => s.numarLegitimatie)
                       .ToList();
                return sportivi;
            }
            else
            {
                sportivi = repo.GetSportiviIQueryable()
                   .Where(s => s.codClub == id && s.nume.Contains(numeFam) && s.prenume.Contains(prenume))
                   .Select(s => new SportiviAntrenorModel
                   {
                       numarLegitimatie = s.numarLegitimatie,
                       urlPozaProfil = s.urlPozaProfil,
                       nume = s.nume,
                       prenume = s.prenume,
                       gen = s.gen,
                       anNastere = s.dataNastere.Year,
                       email = s.Email
                   })
                   .OrderBy(s => s.numarLegitimatie)
                   .ToList();
                return sportivi;
            }
                
            
        }

        /*public void Update(SportivUpdateModel model)
        {
            var sportiv = repo.GetSportiviIQueryable().FirstOrDefault(s => s.Id == model.Id);
            var club = repo.GetCluburiIQueryable().FirstOrDefault(c => c.nume == model.numeClub);
            var antrenor = repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Email == model.antrenorNou);
            if (sportiv == null || club == null || antrenor == null || club.codClub != antrenor.codClub) //daca antrenorul nu e in clubul ales atunci nu se updateaza
                return;
            sportiv.nume = model.nume;
            sportiv.prenume = model.prenume;
            sportiv.abonareStiri = model.abonareStiri;
            sportiv.urlPozaProfil = model.urlPozaProfil;
            sportiv.codClub = club.codClub;
            sportiv.codAntrenor = antrenor.Id;

            repo.Update(sportiv);
        }*/

        public void Update(SportivEmailUpdateModel model)
        {
            var sportiv = repo.GetSportiviIQueryable().FirstOrDefault(s => s.Email == model.emailSportiv);//sportivul caruia ii facem update

            var club = repo.GetCluburiIQueryable().FirstOrDefault(c => c.nume == model.numeClub);//clubul din model

            var antrenor = repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Email == model.antrenorNou);
            if (sportiv == null || club == null || antrenor == null || club.codClub != antrenor.codClub) //daca antrenorul nu e in clubul ales atunci nu se updateaza
                return;


            //daca clubul din model este diferit de clubul initial trebuie sa adaugam
            //sportivId, codClub, dataInscreireClub si parasireClub in tabelul IstoricClub pentru clubul vechi
            if (sportiv.codClub != club.codClub)
            {
                var istoricClub = new IstoricClub
                {
                    codClub = sportiv.codClub,
                    codUtilizator = sportiv.Id,
                    dataInscriereClub = sportiv.dataInscreireClubActual,
                    dataParasireClub = DateTime.Today.AddDays(-1)
                };
                repoIstCl.Create(istoricClub);


                //dupa ce am adaugat in istoric putem face update

                sportiv.nume = model.nume;
                sportiv.prenume = model.prenume;
                sportiv.abonareStiri = model.abonareStiri;
                sportiv.urlPozaProfil = model.urlPozaProfil;
                sportiv.codClub = club.codClub;
                sportiv.codAntrenor = antrenor.Id;
                sportiv.dataInscreireClubActual = DateTime.Now;//pentru ca acum s-a inscris la noul club

                repo.Update(sportiv);
            }
            else
            {
                sportiv.nume = model.nume;
                sportiv.prenume = model.prenume;
                sportiv.abonareStiri = model.abonareStiri;
                sportiv.urlPozaProfil = model.urlPozaProfil;
                sportiv.codClub = club.codClub;
                sportiv.codAntrenor = antrenor.Id;

                repo.Update(sportiv);
            }

        }

        //asa era initial
       /* public void Update(SportivEmailUpdateModel model)
        {
            var sportiv = repo.GetSportiviIQueryable().FirstOrDefault(s => s.Email == model.emailSportiv);//sportivul caruia ii facem update
            var club = repo.GetCluburiIQueryable().FirstOrDefault(c => c.nume == model.numeClub);//clubul din model
            var antrenor = repo.GetAntrenoriIQueryable().FirstOrDefault(a => a.Email == model.antrenorNou);
            if (sportiv == null || club == null || antrenor == null || club.codClub != antrenor.codClub) //daca antrenorul nu e in clubul ales atunci nu se updateaza
                return;
            sportiv.nume = model.nume;
            sportiv.prenume = model.prenume;
            sportiv.abonareStiri = model.abonareStiri;
            sportiv.urlPozaProfil = model.urlPozaProfil;
            sportiv.codClub = club.codClub;
            sportiv.codAntrenor = antrenor.Id;

            repo.Update(sportiv);
        }*/
    }
}
