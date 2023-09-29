using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace GestionareFederatieTriatlon.Manageri
{
    public class LocatieManager: ILocatieManager
    {
        private readonly ILocatieRepo locatieRepo;
        public LocatieManager(ILocatieRepo locatieRepo)
        {
            this.locatieRepo = locatieRepo;
        }

        public List<LocatieModel> GetLocatie()
        {
            var locatii = locatieRepo.GetLocatiiIQueryable();
            if (locatii == null)
            {
                return new List<LocatieModel>();
            }
            var locatiiModel = locatii
                .Select(l => new LocatieModel
                {
                    codLocatie = l.codLocatie,
                    tara = l.tara,
                    oras = l.oras,
                    strada = l.strada,
                    numarStrada = (int)l.numarStrada,
                    detaliiSuplimentare = l.detaliiSuplimentare,
                })
                .OrderBy(l => l.codLocatie)
                .ToList();
            if (locatiiModel.Count > 0)
                return locatiiModel; 

            return locatiiModel;
        }

        public List<LocatieModelById> GetLocatieInfo(int id)
        {
            var locatie = locatieRepo.GetLocatiiIQueryable()
                .Where(l => l.codLocatie == id)
                .Select(l => new LocatieModelById
                {
                    tara = l.tara,
                    oras = l.oras,
                    strada = l.strada,
                    numarStrada = (int)l.numarStrada,
                    detaliiSuplimentare = l.detaliiSuplimentare
                })
                .ToList();
            return locatie;
        }

        // locatieRepo contine include dintre tabelele competitie si locatie si intoarce competitie
        public List<LocatieModelById> GetLocatieGivenComp(int id) //se poate verifica dupa ce fac si crud pentru competitie
        {
            var locatie = locatieRepo.GetLocatiiCompetitie()
                .Where(c => c.codCompetitie == id)
                .Select(l => new LocatieModelById
                {
                    tara = l.Locatie.tara,
                    oras = l.Locatie.oras,
                    strada = l.Locatie.strada,
                    numarStrada = (int)l.Locatie.numarStrada,
                    detaliiSuplimentare = l.Locatie.detaliiSuplimentare
                })
                .ToList();
            return locatie;
        }

        public void Update(LocatieModel locatieModel)
        {
            var locatie = locatieRepo.GetLocatiiIQueryable()
                .FirstOrDefault(l => l.codLocatie == locatieModel.codLocatie);
            if (locatie == null)
                return;
            locatie.tara= locatieModel.tara;
            locatie.oras= locatieModel.oras;
            locatie.strada= locatieModel.strada;
            locatie.numarStrada= locatieModel.numarStrada;
            locatie.detaliiSuplimentare = locatieModel.detaliiSuplimentare;
            locatieRepo.Update(locatie);
        }

        public void Delete(int id)
        {
            var locatie = locatieRepo.GetLocatiiIQueryable()
                .FirstOrDefault(l => l.codLocatie==id);
            if(locatie == null)
                return;
            locatieRepo.Delete(locatie);
        }

        public void Create(LocatieModelById model)
        {
            var locatie = new Locatie
            {
                tara = model.tara,
                oras = model.oras,
                strada = model.strada,
                numarStrada = model.numarStrada,
                detaliiSuplimentare = model.detaliiSuplimentare,
            };
            locatieRepo.Create(locatie);
        }
    }
}
