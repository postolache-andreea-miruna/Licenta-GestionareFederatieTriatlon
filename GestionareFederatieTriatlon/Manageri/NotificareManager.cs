using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Manageri
{
    public class NotificareManager: INotificareManager
    {
        private readonly INotificareRepo repo;
        private readonly ISportivRepo repoSp;
        private readonly UserManager<Utilizator> utilizatorManager;
        public NotificareManager(INotificareRepo repo, ISportivRepo repoSp, UserManager<Utilizator> utilizatorManager)
        {
            this.repo = repo;
            this.repoSp = repoSp;
            this.utilizatorManager = utilizatorManager;
        }
        //functie care modifica citireNotificare la true
        public void NotificareCititaUpdate(int id)// bool citireNotificare)
        {
            var notificare = repo.GetNotificare().Where(n => n.codNotificare == id).SingleOrDefault();
            if(notificare == null)
            {
                return;
            }

            notificare.citireNotificare = true;
            repo.Update(notificare);
        }

        //afisare notificari pt user logat (dupa id utlizator)
        public List<NotificareCodMesajModel> GetCodMesajNotificari()
        {
            var notificari = repo.GetNotificare()
                .Select(n=>new NotificareCodMesajModel
                {
                    codNotificare = n.codNotificare,
                    mesaj = n.mesaj
                })
                .OrderBy(n => n.codNotificare)
                .ToList();
            return notificari;
        }
        public List<NotificariAfisareModel> GetNotificariIdUtilizLogat(string emailUtilizator)
        {

            var utilizatorul = utilizatorManager.FindByEmailAsync(emailUtilizator);
            if (utilizatorul == null)
            {
                return new List<NotificariAfisareModel>();
            }
            var idUtilizator = utilizatorul.Result.Id;

            var utilizator = utilizatorManager.Users;
            var notificari = repo.GetNotificare();
            if (notificari == null)
            {
                return new List<NotificariAfisareModel>();
            }

            var notifModel = notificari
                .Where(n => n.codUtilizator2 == idUtilizator)
                .Select(n => new NotificariAfisareModel
                {
                    nume = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    dataCreare = n.dataCreare,
                    titluNotificare = n.titluNotificare,
                    citireNotificare = n.citireNotificare,
                    codNotificare = n.codNotificare
                })
                .OrderByDescending(n => n.dataCreare)
                .ToList();
            return notifModel;
        }

        public int GetNrNotificariNecititeIdUtilizLogat(string emailUtilizator)
        {
            var utilizatorul = utilizatorManager.FindByEmailAsync(emailUtilizator);
            var idUtilizator = utilizatorul.Result.Id;
            var utilizator = utilizatorManager.Users;
            var notificari = repo.GetNotificare();
            var notifNr = notificari
                .Where(n => n.codUtilizator2 == idUtilizator && n.citireNotificare == false)
                .Count();
            return notifNr;

        }
        /*public List<NotificariAfisareModel> GetNotificariIdUtilizLogat(string idUtilizator)
        {
            var utilizator = utilizatorManager.Users;
            var notificari = repo.GetNotificare();
            if (notificari == null)
            {
                return new List<NotificariAfisareModel>();
            }

            var notifModel = notificari
                .Where(n => n.codUtilizator2 == idUtilizator)
                .Select(n => new NotificariAfisareModel
                {
                    nume = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(n.codUtilizator)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    dataCreare = n.dataCreare,
                    titluNotificare = n.titluNotificare,
                    citireNotificare = n.citireNotificare
                })
                .OrderByDescending(n => n.dataCreare)
                .ToList();
            return notifModel;
        }*/

        //detalii notificare
        public NotificareById GetNotificareById(int id)
        {
            var notificare = repo.GetNotificare()
                .Where(n => n.codNotificare == id)
                .Select(n => new NotificareById
                {
                    mesaj= n.mesaj
                })
                .FirstOrDefault();

            return notificare;
        }

        //create 

        //functie facuta pentru verificare
        public List<ListaGet> GetLista (string emailUtilizator, int numarLegitimatieUtiliz2)////!!!!!! trebuie sa ma asigur ca codUtilizator este antrenorul lui codUtilizator2
        {//verific daca codUtiliz2 se afla printre sportivii antrenati de antrenorul pus in model

            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(emailUtilizator)).Select(u => u.Id).FirstOrDefault();
            var codUtilizator2 = repoSp.GetSportiviIQueryable().Where(u => u.numarLegitimatie.Equals(numarLegitimatieUtiliz2)).Select(u => u.Id).FirstOrDefault();

            string codAntr = codUtilizator;

            /*var utilizatori = utilizatorManager.Users;
             var CodDeAntrenor = utilizatori.Where(u => u.Id.Equals(codAntr)).FirstOrDefault();
             var eCodDeAntrenor = utilizatori.Where(u => u.Id.Equals(codAntr)).Select(u => u.codAntrenor).FirstOrDefault();
             if (eCodDeAntrenor != null)
                { return new List<ListaGet>(); }
            */


            var esteCodDeAntrenor = repoSp.GetAntrenoriIQueryable()     //se verifica daca cel care transmite notificarea este chiar un antrenor
                .Where(a => a.Id == codAntr).Select(a => a.codAntrenor).FirstOrDefault();//a.codAntrenor este nenul doar pentru sportivi (pentru ca acolo se trece codul antrenorului)

            if (esteCodDeAntrenor != null)
                return new List<ListaGet>();

            var esteCodDeSportiv = repoSp.GetSportiviIQueryable() //se verifica daca se trimite mesajul spre un sportiv
                .Where(s => s.Id == codUtilizator2).Select(s => s.codAntrenor).FirstOrDefault();

            if (esteCodDeSportiv == null)
                return new List<ListaGet>();

            //sportivii care il au ca antrenor pe cel care trimite notificarea
            var codSportivi = repoSp.GetSportiviIQueryable()
                    .Where(a => a.codAntrenor.Equals(codAntr))
                    .Select(a => new ListaGet
                    {
                        coduri = a.Id
                    })
                    .ToList();

            bool esteAntrenorulBun = false;
            var x = codSportivi.Count;
            for (int codIndex = 0; codIndex < codSportivi.Count; codIndex++) //se verifica daca sportivul dat este printre sportivii antrenorului
            {
                if (codSportivi[codIndex].coduri == codUtilizator2)
                    esteAntrenorulBun = true;
            }
            if (esteAntrenorulBun != true)
                return new List<ListaGet>();

            return codSportivi;

        }


        public void Create(NotificareCreateModel notifCreate)////!!!!!! trebuie sa ma asigur ca codUtilizator este antrenorul lui codUtilizator2
        {//verific daca codUtiliz2 se afla printre sportivii antrenati de antrenorul pus in model
            /*            string codAntr = notifCreate.codUtilizator;

                        var esteCodDeAntrenor = repoSp.GetAntrenoriIQueryable()     //se verifica daca cel care transmite notificarea este chiar un antrenor
                            .Where(a => a.Id == codAntr).Select(a => a.codAntrenor);//a.codAntrenor este nenul doar pentru sportivi (pentru ca acolo se trece codul antrenorului)

                        if (esteCodDeAntrenor != null)
                            return;

                        var esteCodDeSportiv = repoSp.GetSportiviIQueryable() //se verifica daca se trimite mesajul spre un sportiv
                            .Where(s => s.Id == notifCreate.codUtilizator2).Select(s => s.codAntrenor);

                        if (esteCodDeSportiv == null)
                            return;

                        //sportivii care il au ca antrenor pe cel care trimite notificarea
                        List<string> codSportivi = repoSp.GetSportiviIQueryable()
                                .Where(a => a.codAntrenor.Equals(codAntr)).Select(a => a.codAntrenor).ToList();

                        bool esteAntrenorulBun = false;

                        for (int codIndex = 1; codIndex <= codSportivi.Count; codIndex++)
                        {
                            if (codSportivi[codIndex] == notifCreate.codUtilizator2)
                                esteAntrenorulBun = true;
                        }
                        if (esteAntrenorulBun != true)
                            return;*/


            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(notifCreate.emailUtilizator)).Select(u => u.Id).FirstOrDefault();
            
            var codUtilizator2 = repoSp.GetSportiviIQueryable().Where(u => u.numarLegitimatie.Equals(notifCreate.numarLegitimatieUtiliz2)).Select(u => u.Id).FirstOrDefault();

            string codAntr = codUtilizator;

            var esteCodDeAntrenor = repoSp.GetAntrenoriIQueryable()     //se verifica daca cel care transmite notificarea este chiar un antrenor
                .Where(a => a.Id == codAntr).Select(a => a.codAntrenor).FirstOrDefault();//a.codAntrenor este nenul doar pentru sportivi (pentru ca acolo se trece codul antrenorului)

            if (esteCodDeAntrenor != null)
                return;

            var esteCodDeSportiv = repoSp.GetSportiviIQueryable() //se verifica daca se trimite mesajul spre un sportiv
                .Where(s => s.Id == codUtilizator2).Select(s => s.codAntrenor).FirstOrDefault();

            if (esteCodDeSportiv == null)
                return;

            //sportivii care il au ca antrenor pe cel care trimite notificarea
            var codSportivi = repoSp.GetSportiviIQueryable()
                    .Where(a => a.codAntrenor.Equals(codAntr))
                    .Select(a => new ListaGet
                    {
                        coduri = a.Id
                    })
                    .ToList();

            bool esteAntrenorulBun = false;
            var x = codSportivi.Count;
            for (int codIndex = 0; codIndex < codSportivi.Count; codIndex++) //se verifica daca sportivul dat este printre sportivii antrenorului
            {
                if (codSportivi[codIndex].coduri == codUtilizator2)
                    esteAntrenorulBun = true;
            }
            if (esteAntrenorulBun != true)
                return;



            var newNotif = new Notificare
            {
                mesaj = notifCreate.mesaj,
                codUtilizator2 = codUtilizator2,
                dataCreare = DateTime.Now,
                titluNotificare = notifCreate.titluNotificare,
                citireNotificare = false,
                codUtilizator = codUtilizator
            };
            repo.Create(newNotif);
        }
        public void Delete(int id)
        {
            var notificare = repo.GetNotificare()
                .FirstOrDefault(n => n.codNotificare == id);
            if (notificare == null) return;
            repo.Delete(notificare);
        }

        /*        public void Create(NotificareCreateModel notifCreate)////!!!!!! trebuie sa ma asigur ca codUtilizator este antrenorul lui codUtilizator2
                {//verific daca codUtiliz2 se afla printre sportivii antrenati de antrenorul pus in model
                    string codAntr = notifCreate.codUtilizator;

                    var esteCodDeAntrenor = repoSp.GetAntrenoriIQueryable()     //se verifica daca cel care transmite notificarea este chiar un antrenor
                        .Where(a => a.Id == codAntr).Select(a => a.codAntrenor);//a.codAntrenor este nenul doar pentru sportivi (pentru ca acolo se trece codul antrenorului)

                    if (esteCodDeAntrenor != null)
                        return;

                    var esteCodDeSportiv = repoSp.GetSportiviIQueryable() //se verifica daca se trimite mesajul spre un sportiv
                        .Where(s => s.Id == notifCreate.codUtilizator2).Select(s => s.codAntrenor);

                    if (esteCodDeSportiv == null)
                        return;

                    //sportivii care il au ca antrenor pe cel care trimite notificarea
                    List<string> codSportivi = repoSp.GetSportiviIQueryable()
                            .Where(a => a.codAntrenor.Equals(codAntr)).Select(a => a.codAntrenor).ToList();

                    bool esteAntrenorulBun = false;

                    for (int codIndex = 1; codIndex <= codSportivi.Count; codIndex++)
                    {
                        if (codSportivi[codIndex] == notifCreate.codUtilizator2)
                            esteAntrenorulBun = true;
                    }
                    if (esteAntrenorulBun != true)
                        return;

                    var newNotif = new Notificare
                    {
                        mesaj = notifCreate.mesaj,
                        codUtilizator2 = notifCreate.codUtilizator2,
                        dataCreare = DateTime.Now,
                        titluNotificare = notifCreate.titluNotificare,
                        citireNotificare = false,
                        codUtilizator = notifCreate.codUtilizator
                    };
                    repo.Create(newNotif);
                }*/
    }
}
