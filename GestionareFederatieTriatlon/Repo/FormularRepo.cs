using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class FormularRepo: IFormularRepo
    {
        private GestionareFederatieTriatlonContext db;
        public FormularRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }

        public IQueryable<Formular> GetFormularIQueryable()
        {
            var formulare = db.Formulare;
            return formulare;
        }
        public IQueryable<Sportiv> GetSportivi()
        {
            var sportiv = db.Sportivi;
            return sportiv;
        }

        public void Create(Formular form)
        {
            db.Formulare.Add(form);
            db.SaveChanges();
        }

        public void Update(Formular form)
        {
            db.Formulare.Update(form);
            db.SaveChanges();
        }
    }
}
