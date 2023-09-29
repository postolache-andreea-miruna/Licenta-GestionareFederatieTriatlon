using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.EntityFrameworkCore;

namespace GestionareFederatieTriatlon.Manageri
{
    public class ClubManager : IClubManager
    {
        private readonly IClubRepo clubRepo;
        public ClubManager(IClubRepo clubRepo)
        {
            this.clubRepo = clubRepo;
        }

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
                    codClub = c.codClub,
                    nume = c.nume,
                    email= c.email,
                    urlPozaClub = c.urlPozaClub,
                })
                .OrderBy(c => c.nume)
                .ToList();
            if (cluburiModel.Count > 0) { return cluburiModel; }
            return cluburiModel;
        }

        public List<ClubModelTotal?> GetCluburi()
        {
            var cluburi = clubRepo.GetCluburiIQueryable();
            if (cluburi == null)
            {
                return new List<ClubModelTotal>();
            }

            var cluburiModel = cluburi
                .Select(c => new ClubModelTotal
                {
                    codClub = c.codClub,
                    nume = c.nume,
                    email = c.email,
                    urlPozaClub = c.urlPozaClub,
                    descriere = c.descriere
                })
                .OrderBy(c => c.nume)
                .ToList();
            if (cluburiModel.Count > 0) { return cluburiModel; }
            return cluburiModel;
        }

        public List<string> GetNumeCluburi()
        {
            var cluburi = clubRepo.GetCluburiIQueryable();
            if (cluburi == null)
            {
                return new List<string>();
            }

            var cluburiModel = cluburi
                                 .Select(c =>  c.nume)
                                 .ToList();
            return cluburiModel;
        }

        public List<ClubModelById> GetClubInfo(int id)
        {
            var club = clubRepo.GetCluburiIQueryable()
                .Where(c => c.codClub == id)
                .Select(c => new ClubModelById
                {
                    nume = c.nume,
                    email = c.email,
                    descriere = c.descriere,
                    urlPozaClub = c.urlPozaClub,
                })
                .ToList();
            return club;
        }

        public void Update(ClubUpdateModel clubUpdateModel)
        {
            var club = clubRepo.GetCluburiIQueryable()
                .FirstOrDefault(x => x.codClub == clubUpdateModel.codClub);
            if (club == null)
                return;
            club.codClub = clubUpdateModel.codClub;
            club.nume = clubUpdateModel.nume;
            club.descriere = clubUpdateModel.descriere;
            club.email = clubUpdateModel.email;
            club.urlPozaClub = clubUpdateModel.urlPozaClub;
            clubRepo.Update(club);
        }

        public void Delete(int id)
        {
            var club = clubRepo.GetCluburiIQueryable()
                .FirstOrDefault(c =>c.codClub == id);
            if (club == null)
                return;
            clubRepo.Delete(club);
        }

        public void Create(ClubModelById clubCreate)
        {
            var newClub = new Club
            {
                nume = clubCreate.nume,
                email = clubCreate.email,
                descriere = clubCreate.descriere,
                urlPozaClub = clubCreate.urlPozaClub,
            };
            clubRepo.Create(newClub);
        }
    }

}
