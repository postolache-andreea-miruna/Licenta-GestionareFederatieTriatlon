using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class IstoricClubRepo:IIstoricClubRepo
    {
        private GestionareFederatieTriatlonContext db;
        public IstoricClubRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }
        public void Create(IstoricClub istoricClub)
        {
            db.IstoriceCluburi.Add(istoricClub);
            db.SaveChanges();
        }

        public IQueryable<IstoricClub> GetIstoricCluburi()
        {
            var istoriceClub = db.IstoriceCluburi;
            return istoriceClub;
        }
    }
}
