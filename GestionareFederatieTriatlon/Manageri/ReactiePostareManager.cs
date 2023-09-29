using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Identity;

namespace GestionareFederatieTriatlon.Manageri
{
    public class ReactiePostareManager:IReactiePostareManager
    {
        private readonly IReactiePostareRepo reactieRepo;
        private readonly UserManager<Utilizator> utilizatorManager;
        public ReactiePostareManager(IReactiePostareRepo reactieRepo, UserManager<Utilizator> utilizatorManager)
        {
            this.reactieRepo = reactieRepo;
            this.utilizatorManager = utilizatorManager;
        }

        public void Create(ReactiePostareCreateModel model)
        {
            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(model.emailUtilizator)).Select(u => u.Id).FirstOrDefault();
            
            var newReactie = new ReactiePostare
            {
                codUtilizator = codUtilizator,
                codPostare = model.codPostare,
                reactieFericire = model.reactieFericire,
                reactieTristete = model.reactieTristete,
            };
            reactieRepo.Create(newReactie);
        }

        public void UpdateFericire(ReactiePostareUpdateFericireModel model)
        {
            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(model.emailUtilizator)).Select(u => u.Id).FirstOrDefault();


            var reactie = reactieRepo.GetReactiiPostareIQueryable()
                .FirstOrDefault(x => x.codPostare == model.codPostare && x.codUtilizator == codUtilizator);
            if (reactie == null)
                return;

            reactie.reactieFericire = model.reactieFericire;
            reactieRepo.Update(reactie);
        }

        public void UpdateTristete(ReactiePostareUpdateTristeteModel model)
        {
            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(model.emailUtilizator)).Select(u => u.Id).FirstOrDefault();


            var reactie = reactieRepo.GetReactiiPostareIQueryable()
                .FirstOrDefault(x => x.codPostare == model.codPostare && x.codUtilizator == codUtilizator);
            if (reactie == null)
                return;

            reactie.reactieTristete = model.reactieTristete;
            reactieRepo.Update(reactie);
        }

        public ReactiiModel? GetReactiiForUserPost(string emailUtilizator, int codPostare)
        {
            var utilizatori = utilizatorManager.Users;
            var codUtilizator = utilizatori.Where(u => u.Email.Equals(emailUtilizator)).Select(u => u.Id).FirstOrDefault();

            var reactie = reactieRepo.GetReactiiPostareIQueryable()
                .FirstOrDefault(x => x.codPostare == codPostare && x.codUtilizator == codUtilizator);
            if (reactie == null)
                return null;

            ReactiiModel reactia = new ReactiiModel();
            reactia.reactieFericire = reactie.reactieFericire;
            reactia.reactieTristete = reactie.reactieTristete;

            return reactia;
        }

        public int GetNrTotalFericirePostare(int codPostare)
        {
            var nr  = reactieRepo.GetReactiiPostareIQueryable()
                .Where(r => r.codPostare == codPostare && r.reactieFericire == true)
                .Count();
            return nr;
        }

        public int GetNrTotalTristetePostare(int codPostare)
        {
            var nr = reactieRepo.GetReactiiPostareIQueryable()
                .Where(r => r.codPostare == codPostare && r.reactieTristete == true)
                .Count();
            return nr;
        }
    }
}
