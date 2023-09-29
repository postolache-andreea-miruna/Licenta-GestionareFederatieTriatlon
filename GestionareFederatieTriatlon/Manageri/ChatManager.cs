using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Manageri
{
    public class ChatManager: IChatManager
    {
        private readonly IChatRepo repo;
        private readonly UserManager<Utilizator> userManager;
        private readonly GestionareFederatieTriatlonContext db;
  
        public ChatManager(IChatRepo repo, UserManager<Utilizator> userManager, GestionareFederatieTriatlonContext db)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.db = db;
        }

        public void UpdateCodConexiune(string idUtilizator,string conexiune)
        {

            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));
            if (utilizator == null)
                return;
            utilizator.codConexiune= conexiune;
            repo.Update(utilizator);
        }
        /*public string GetNume(string idUtilizator)
        {
            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));
            if (utilizator == null)
                return "";
            var nume = utilizator.nume;
            return nume;
        }*/
        public NumeUtilizatorChat GetNume(string idUtilizator)
        {
            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));
           
            if (utilizator == null)
            {
                return new NumeUtilizatorChat();
            }
            var utilizatorNume = userManager.Users
                    .Where(u => u.Id.Equals(idUtilizator))
                    .Select(s => new NumeUtilizatorChat
                     {nume = utilizator.nume}).FirstOrDefault();
            return utilizatorNume;
        }
        public PrenumeUtilizatorChat GetPrenume(string idUtilizator)
        {
            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));

            if (utilizator == null)
            {
                return new PrenumeUtilizatorChat();
            }
            var utilizatorPrenume = userManager.Users
                    .Where(u => u.Id.Equals(idUtilizator))
                    .Select(s => new PrenumeUtilizatorChat
                    { prenume = utilizator.prenume }).FirstOrDefault();
            return utilizatorPrenume;
        }
        /*public string GetPrenume(string idUtilizator)
        {
            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));
            if (utilizator == null)
                return "";
            var prenume = utilizator.prenume;
            return prenume;
        }*/
        public bool GetDisponibilitate(string idUtilizator)
        {
            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));

            var disponibilitate = utilizator.disponibilitate;
            return disponibilitate;
        }
        public void UpdateDisponibilitate(string idUtilizator, bool disponibilitate)
        {

            var utilizator = userManager.Users
                .FirstOrDefault(u => u.Id.Equals(idUtilizator));
            if (utilizator == null)
                return;
            utilizator.disponibilitate = disponibilitate;
            repo.Update(utilizator);
        }

        /*        public void CreateChat()
                {
                    var newChat = new Chat
                    {
                        dataInfiintarii = DateTime.Now,
                    };
                    repo.CreateChat(newChat);
                }*/

        /*public void CreateConversatie(ConversatieCreateModel model)
        {
            int maxCodConversatie = 0;
            int nrCodConversatii = repo.GetConversatii().Count();
            if(nrCodConversatii == 0)
            {
                maxCodConversatie = 0;
            }
            else
            {
                maxCodConversatie = repo.GetConversatii().Max(c => c.codConversatie);
            }
            var newConversatie = new Conversatie
            {
               codConversatie = maxCodConversatie + 1,
               codUtilizator = model.codUtilizator,
               codChat = model.codChat,
               codUtilizator2= model.codUtilizator2,
               mesajConversatie = model.mesajConversatie,
               dataTrimitereMesaj = DateTime.Now
            };
            repo.CreateConversatie(newConversatie);
        }*/

        public void CreateMesaj(ConversatieCreateModel model)
        {

            var newMesaj = new Mesaj
            {
                codUtilizator = model.codUtilizator,
                codUtilizator2 = model.codUtilizator2,
                mesajConversatie = model.mesajConversatie,
                dataTrimitereMesaj = DateTime.Now,
                citireMesaj = model.citireMesaj
            };
            repo.CreateMesaj(newMesaj);
        }

        public void UpdateCitireMesaj(string idUtilizator,string idUtilizator2, bool citireMesaj)
        {

            var mesaje = repo.GetMesaje()
                .Where(m => (m.codUtilizator == idUtilizator && m.codUtilizator2 == idUtilizator2 && m.citireMesaj == false) 
                            || (m.codUtilizator2 == idUtilizator && m.codUtilizator == idUtilizator2 && m.citireMesaj == false)
                      )
                .ToList();

          mesaje.ForEach(m=> m.citireMesaj = citireMesaj);
          db.SaveChanges();
            //repo.Update(utilizator);
        }

        public void UpdateCitireMesajSpreUserNelogat(string idUtilizator, string idUtilizator2, bool citireMesaj)
        {

            var mesaje = repo.GetMesaje()
                .Where(m =>  m.codUtilizator2 == idUtilizator && m.codUtilizator == idUtilizator2 && m.citireMesaj == false
                      )
                .ToList();

            mesaje.ForEach(m => m.citireMesaj = citireMesaj);
            db.SaveChanges();
            //repo.Update(utilizator);
        }

        public List<MesajAfisare> GetMesaje(string utilizator, string utilizator2)
        {
            var mesaje = repo.GetMesaje().Where(m => m.codUtilizator == utilizator && m.codUtilizator2 == utilizator2);
            var mesajeToFrom = repo.GetMesaje().Where(m => m.codUtilizator == utilizator2 && m.codUtilizator2 == utilizator);
            var mesajeUnion = mesaje.Union(mesajeToFrom);

            if (mesajeUnion == null)
                return new List<MesajAfisare>();

            var mesajeAfis = mesajeUnion.Select(m => new MesajAfisare
            {
                /*codUtilizator= m.codUtilizator,
                codUtilizator2= m.codUtilizator2,*/
                emailUtilizator = userManager.Users.Where(u => u.Id == m.codUtilizator).Select(u => u.Email).FirstOrDefault(),
                emailUtilizator2 = userManager.Users.Where(u => u.Id == m.codUtilizator2).Select(u => u.Email).FirstOrDefault(),
                mesajConversatie = m.mesajConversatie,
                dataTrimitereMesaj = m.dataTrimitereMesaj
            })
                .OrderBy(m => m.dataTrimitereMesaj)
                .ToList();
           
            return mesajeAfis;
        }
        /*
         public List<ClubModel?> GetClub()
        {
               var cluburi = clubRepo.GetCluburiIQueryable();
               if (cluburi == null)
               {
                   return new List<ClubModel>();
               }

            var cluburiModel = cluburi
                .Select(c => new ClubModel
                {
                    nume = c.nume,
                    email= c.email 
                })
                .OrderBy(c => c.nume)
                .ToList();
            if (cluburiModel.Count > 0) { return cluburiModel; }
            return cluburiModel;
        }
         */

        public List<string> OnlineUser()
        {
            var utilizatoriOnline = userManager.Users.Where(u => u.codConexiune != null).Select(u => u.Email).ToList();
            return utilizatoriOnline;
        }


        /*        public List<OnlineUser> UsersOnline()
                {
                    var utilizatoriOnline = userManager.Users.Where(u => u.codConexiune != null).Select(u => new OnlineUser
                    {
                        nume = u.nume,
                        prenume= u.prenume,
                        disponibilitate= u.disponibilitate,
                        Email = u.Email
                    }).ToList();

                    return utilizatoriOnline;
                }*/

        public List<OnlineUser> UsersOnline()
        {
            var utilizatoriOnline = userManager.Users.Where(u => u.codConexiune != null).Select(u => new OnlineUser
            {
                nume = u.nume,
                prenume = u.prenume,
                disponibilitate = u.disponibilitate,
                Email = u.Email
            })
                .OrderBy(u => u.nume)
                .ToList();
            return utilizatoriOnline;
        }


        public int NumarMesajeNecitite(string emailUser)
        {
            var nrMesajeNecitite = 0;
            var utilizatorConectat = userManager.Users
               .Where(u => u.Email.Equals(emailUser))
               .FirstOrDefault();
            var utilizatori = userManager.Users
                .Where(u => u.Email != emailUser)
                .Select(u => new 
                {
                    id = u.Id,
                })
                .ToList();

            for (int i = 0; i < utilizatori.Count(); i++)
            {
                //cate mesaje au fost trimise de userul logat spre cei logati pana in momentul actual
                var numarMesajeNecitite = repo.GetMesaje()
                    .Where(m => m.codUtilizator == utilizatori[i].id
                            && m.codUtilizator2 == utilizatorConectat.Id
                            && m.citireMesaj == false)
                    .Count();

                nrMesajeNecitite += numarMesajeNecitite;
            }
            return nrMesajeNecitite;
        }
        public List<UtilizatoriOnlineSiMesaje> MessagesUnreadFromUsersByEmail(string emailUser)
        {
            var utilizatorConectat = userManager.Users
                .Where(u => u.Email.Equals(emailUser))
                .FirstOrDefault();

            var utilizatori = userManager.Users
                .Where(u =>  u.Email != emailUser)
                .Select(u => new UtilizatoriOnlineSiMesaje
                {
                    id = u.Id,
                    nume = u.nume,
                    prenume = u.prenume,
                    disponibilitate = u.disponibilitate,
                    Email = u.Email,
                    nrMesaje = 0

                })
                .ToList();

            // var utilizOnlineMesaje = new List<UtilizatoriOnlineSiMesaje>();
            //avem toti userii Online
            for (int i = 0; i < utilizatori.Count(); i++)
            {
                //cate mesaje au fost trimise de userul logat spre cei logati pana in momentul actual
                var numarMesajeNecitite = repo.GetMesaje()
                    .Where(m => m.codUtilizator == utilizatori[i].id
                            && m.codUtilizator2 == utilizatorConectat.Id
                            && m.citireMesaj == false)
                    .Count();

                utilizatori[i].nrMesaje = numarMesajeNecitite;
            }

            var ultimulUtiliz = new UtilizatoriOnlineSiMesaje();
            ultimulUtiliz.id = utilizatorConectat.Id;
            ultimulUtiliz.Email = emailUser;
            ultimulUtiliz.nume = utilizatorConectat.nume;
            ultimulUtiliz.prenume = utilizatorConectat.prenume;
            ultimulUtiliz.disponibilitate = utilizatorConectat.disponibilitate;
            ultimulUtiliz.nrMesaje = 0;

            utilizatori.Add(ultimulUtiliz);

            //vrem sa eliminam cele care au nrMesaje = 0;

            var utilizatoriMesajeNecitite = utilizatori.Where(u => u.nrMesaje >0)
                .OrderBy(u => u.nume)
                .ToList();

            // return utilizatori;
            return utilizatoriMesajeNecitite;

        }

        public List<OnlineUser> UsersOffline()
        {
            var utilizatoriOffline = userManager.Users.Where(u => u.codConexiune == null && !u.RoluriUtilizatori.Any(r=>r.Rol.Name == "AdminUtilizator")).Select(u => new OnlineUser
            {
                nume = u.nume,
                prenume = u.prenume,
                disponibilitate = u.disponibilitate,
                Email = u.Email
            })
                .OrderBy(u => u.nume)
                .ToList();
            return utilizatoriOffline;
        }
    }
}
