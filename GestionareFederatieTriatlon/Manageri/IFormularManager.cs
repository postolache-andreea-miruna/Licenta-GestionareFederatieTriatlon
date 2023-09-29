using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IFormularManager
    {
        void Create(FormularCreateModel formCreate);
        FormularByIdModel GetFormInfo(int id);
        List<FormularModel> GetFormulareForSportiv(int nrlegitimatie);
        List<FormularModel> GetFormulareForSportivByEmail(string email);
        List<FormularModelTotal> GetAllFormualre(string an="toti anii");
    }
}
