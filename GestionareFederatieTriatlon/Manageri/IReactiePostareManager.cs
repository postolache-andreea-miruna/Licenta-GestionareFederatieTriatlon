using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IReactiePostareManager
    {
        void Create(ReactiePostareCreateModel model);
        void UpdateFericire(ReactiePostareUpdateFericireModel model);
        void UpdateTristete(ReactiePostareUpdateTristeteModel model);
        ReactiiModel GetReactiiForUserPost(string emailUtilizator, int codPostare);
        int GetNrTotalFericirePostare(int codPostare);
        int GetNrTotalTristetePostare(int codPostare);
    }
}
