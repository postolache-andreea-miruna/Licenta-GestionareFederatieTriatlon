using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using System;
using System.Globalization;

namespace GestionareFederatieTriatlon.Manageri
{
    public class FormularManager: IFormularManager
    {
        private readonly IFormularRepo repo;
        public FormularManager(IFormularRepo repo)
        {
            this.repo = repo;
        }

        public void Create(FormularCreateModel formCreate)
        {
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == formCreate.numarDeLegitimatie);
            if (sportiv == null)
                return;
            var newForm = new Formular
            {
                pozaProfil = formCreate.pozaDeProfil,
                avizMedical = formCreate.avizMedical,
                buletin_CertificatNastere = formCreate.buletin_CertificatNastere,
                codUtilizator = sportiv.Id
            };
            repo.Create(newForm);
        }

        public FormularByIdModel GetFormInfo(int id)
        {
            var form = repo.GetFormularIQueryable()
                .Where(f => f.codFormular == id)
                .Select(f => new FormularByIdModel
                {
                    pozaProfil = f.pozaProfil,
                    avizMedical = f.avizMedical,
                    buletin_CertificatNastere = f.buletin_CertificatNastere,
                    completareFormular = f.completareFormular.ToString("yyyy-MM-dd")
                })
                .FirstOrDefault();
            return form;
        }
        public List<FormularModelTotal> GetAllFormualre(string an="toti anii")
        {
            if(an == "toti anii")
            {
                var form = repo.GetFormularIQueryable()
               .Select(f => new FormularModelTotal
               {
                   codFormular = f.codFormular,
                   pozaProfil = f.pozaProfil,
                   avizMedical = f.avizMedical,
                   buletin_CertificatNastere = f.buletin_CertificatNastere
               })
               .OrderByDescending(f => f.codFormular)
               .ToList();
                return form;
            }
            else
            {
                var form = repo.GetFormularIQueryable()
               .Where(f => f.completareFormular.Year == Convert.ToInt32(an))
               .Select(f => new FormularModelTotal
               {
                   codFormular = f.codFormular,
                   pozaProfil = f.pozaProfil,
                   avizMedical = f.avizMedical,
                   buletin_CertificatNastere = f.buletin_CertificatNastere
               })
               .OrderByDescending(f => f.codFormular)
               .ToList();
                return form;
            }
           
        }

        public List<FormularModel> GetFormulareForSportiv(int nrlegitimatie)
        {
            var cultureInfo = new CultureInfo("zh-CN");
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.numarLegitimatie == nrlegitimatie);
            if (sportiv == null)
                return new List<FormularModel>();

            var formulare = repo.GetFormularIQueryable()
                .Where(f => f.codUtilizator == sportiv.Id)
                .Select(f => new FormularModel
                {
                    codFormular = f.codFormular,
                    completareForm = f.completareFormular.ToString("yyyy-MM-dd", cultureInfo)
                })
               .OrderBy(f => f.codFormular)
                .ToList();
            return formulare;
        }

        public List<FormularModel> GetFormulareForSportivByEmail(string email)
        {
            var cultureInfo = new CultureInfo("zh-CN");
            var sportiv = repo.GetSportivi().FirstOrDefault(i => i.Email == email);
            if (sportiv == null)
                return new List<FormularModel>();

            var formulare = repo.GetFormularIQueryable()
                .Where(f => f.codUtilizator == sportiv.Id)
                .Select(f => new FormularModel
                {
                    codFormular = f.codFormular,
                    completareForm = f.completareFormular.ToString("yyyy-MM-dd", cultureInfo)
                })
               .OrderByDescending(f => f.codFormular)
                .ToList();
            return formulare;
        }

    }//CultureInfo enUS = new CultureInfo("en-US");
    //DateTime date = DateTime.ParseExact("12/31/1999", "MM/dd/yyyy", enUS);
}
