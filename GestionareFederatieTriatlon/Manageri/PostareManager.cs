using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Manageri
{
    public class PostareManager: IPostareManager
    {
        private readonly IPostareRepo repo;
        private readonly IReactiePostareRepo repoReactie;
        private readonly IComentariuRepo comRepo;
        private readonly UserManager<Utilizator> utilizatorManager;
        public PostareManager(IPostareRepo repo, IComentariuRepo comRepo,UserManager<Utilizator> utilizatorManager, IReactiePostareRepo repoReactie)
        {
            this.repo = repo;
            this.comRepo = comRepo;
            this.utilizatorManager = utilizatorManager;
            this.repoReactie = repoReactie;
        }

        public void Delete(int id)
        {
            var postare = repo.GetPostari()
                .FirstOrDefault(p => p.codPostare == id);
            var reactii = repoReactie.GetReactiiPostareIQueryable()
                .Where(r => r.codPostare == id)
                .ToList();
            var comentarii = comRepo.GetComentariiIQueryable()
                .Where(c => c.codPostare == id)
                .ToList();
            foreach(var reactie in reactii)
            {
                repoReactie.Delete(reactie);
            }

            foreach(var com in comentarii)
            {
                comRepo.Delete(com);
            }

            if (postare == null)
                return;

            repo.Delete(postare);
        }

        public List<PostareModelTotal> GetAllPostari()
        {
            var postare = repo.GetPostari()
                .Select(p => new PostareModelTotal
                {
                    codPostare = p.codPostare,
                    urlPoza= p.urlPoza
                })
                .ToList();
            return postare;
        }
        public List<PostareByIdUtilizModel> GetPostariIdUtiliz(string idUtiliz)
        {
            var postare = repo.GetPostari()
                .Where(p => p.codUtilizator.Equals(idUtiliz))
                .Select(p => new PostareByIdUtilizModel
                {
                    urlPoza = p.urlPoza,
                    numarReactiiFericire = p.numarReactiiFericire,
                    numarReactiiTristete = p.numarReactiiTristete,
                    dataPostare = p.dataPostare,
                })
                .OrderByDescending(p => p.dataPostare)
                .ToList();
            return postare; 
        }

        public List<PostareByEmailUtilizModel> GetPostariEmailUtiliz(string email)//postarile utilizatorului cu email dat
        {
            var comentarii = comRepo.GetComentariiIQueryable();

            var utilizatorul = utilizatorManager.FindByEmailAsync(email);
            if (utilizatorul == null)
            {
                return new List<PostareByEmailUtilizModel>();
            }
            var idUtiliz = utilizatorul.Result.Id;
            var utilizator = utilizatorManager.Users;
            var postare = repo.GetPostari()
                .Where(p => p.codUtilizator.Equals(idUtiliz))
                .Select(p => new PostareByEmailUtilizModel
                {
                    urlPoza = p.urlPoza,
                    numarReactiiFericire = p.numarReactiiFericire,
                    numarReactiiTristete = p.numarReactiiTristete,
                    dataPostare = p.dataPostare,
                    descriere = p.descriere,

                    nume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    email = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.Email).FirstOrDefault(),


                    codPostare = p.codPostare,
                    comentarii = comentarii.Where(c => c.codPostare == p.codPostare)
                                            .Select(c => new ComentariiModel
                                            {
                                                mesajComentariu = c.mesajComentariu,
                                                dataComentariu = c.dataComentariu,
                                                nume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.nume).FirstOrDefault(),
                                                prenume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.prenume).FirstOrDefault(),
                                                urlPozaProfil = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                                                codComentariu = c.codComentariu,
                                                emailUtilizatorComentariu = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.Email).FirstOrDefault()


                                            })
                .OrderByDescending(c => c.dataComentariu)
                .ToList()//GetComentariiByIdPost(p.codPostare)

                })
                .OrderByDescending(p => p.dataPostare)
                .ToList();
            return postare;
        }

        public List<PostariUtilizatori> GetPostariVazuteDeIdUtiliz(string idUtiliz) //aici o sa fie id utilizator care e logat (el nu tb sa vada in feed pozele lui)
        {
            var utilizator = utilizatorManager.Users;
            var postari = repo.GetPostari()
                .Where(p => p.codUtilizator != idUtiliz)
                .Select(p => new PostariUtilizatori
                {
                    urlPoza = p.urlPoza,
                    numarReactiiFericire = p.numarReactiiFericire,
                    numarReactiiTristete = p.numarReactiiTristete,
                    dataPostare = p.dataPostare,
                    nume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.urlPozaProfil).FirstOrDefault()
                })
                .OrderByDescending(p => p.dataPostare)
                .ToList();
            return postari;                
        }

        public List <PostareComentarii> GetPostariVazuteDeIdUtilizComentarii(string email) //aici o sa fie id utilizator care e logat (el nu tb sa vada in feed pozele lui)
        {
            var comentarii = comRepo.GetComentariiIQueryable();
            var utilizatorul = utilizatorManager.FindByEmailAsync(email);
            if (utilizatorul == null)
            {
                return new List<PostareComentarii>();
            }
            var idUtiliz = utilizatorul.Result.Id;

            var utilizator = utilizatorManager.Users;
            var postari = repo.GetPostari()
                .Where(p => p.codUtilizator != idUtiliz)
                .Select(p => new PostareComentarii
                {
                    urlPoza = p.urlPoza,
                    numarReactiiFericire = p.numarReactiiFericire,
                    numarReactiiTristete = p.numarReactiiTristete,
                    dataPostare = p.dataPostare,
                    descriere = p.descriere,
                    nume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    email = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.Email).FirstOrDefault(),
                    codAntrenor = utilizator.Where(u => u.Id.Equals(p.codUtilizator)).Select(u => u.codAntrenor).FirstOrDefault(),
                    codPostare = p.codPostare,
                    comentarii = comentarii.Where(c => c.codPostare == p.codPostare)
                                            .Select(c => new ComentariiModel
                {
                    mesajComentariu = c.mesajComentariu,
                    dataComentariu = c.dataComentariu,
                    nume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.nume).FirstOrDefault(),
                    prenume = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.prenume).FirstOrDefault(),
                    urlPozaProfil = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.urlPozaProfil).FirstOrDefault(),
                    codComentariu = c.codComentariu,
                   emailUtilizatorComentariu = utilizator.Where(u => u.Id.Equals(c.codUtilizatorComentariu)).Select(u => u.Email).FirstOrDefault()

                })
                .OrderByDescending(c => c.dataComentariu)
                .ToList()//GetComentariiByIdPost(p.codPostare)
        })
        
                .OrderByDescending(p => p.dataPostare)
                .ToList();
            return postari;
        }


        public void UpdateFericireCresc(int codPostare)
        {
            var postare = repo.GetPostari()
                .FirstOrDefault(p => p.codPostare == codPostare);

            if (postare == null) 
                return;

            postare.numarReactiiFericire += 1;
            repo.Update(postare);
        }
        public void UpdateFericireDesc(int codPostare)
        {
            var postare = repo.GetPostari()
                .FirstOrDefault(p => p.codPostare == codPostare);

            if (postare == null)
                return;

            if(postare.numarReactiiFericire > 0)
                postare.numarReactiiFericire -= 1;

            repo.Update(postare);
        }


        public void UpdateTristeteCresc(int codPostare)
        {
            var postare = repo.GetPostari()
                .FirstOrDefault(p => p.codPostare == codPostare);

            if (postare == null)
                return;

            postare.numarReactiiTristete += 1;
            repo.Update(postare);
        }

        public void UpdateTristeteDesc(int codPostare)
        {
            var postare = repo.GetPostari()
                .FirstOrDefault(p => p.codPostare == codPostare);

            if (postare == null)
                return;

            if (postare.numarReactiiTristete > 0)
                postare.numarReactiiTristete -= 1;

            repo.Update(postare);
        }

        public void Create(PostareCreateModel postare)
        {
            var utilizatorul = utilizatorManager.FindByEmailAsync(postare.emailUtilizator);
            if (utilizatorul == null)
            {
                return;
            }
            var codUtilizator = utilizatorul.Result.Id;

            var newPostare = new Postare
            {
                urlPoza = postare.urlPoza,
                numarReactiiFericire = 0,
                numarReactiiTristete = 0,
                dataPostare = DateTime.Now,
                descriere = postare.descriere,
                codUtilizator = codUtilizator
            };
            repo.Create(newPostare);
        }
    }
}
