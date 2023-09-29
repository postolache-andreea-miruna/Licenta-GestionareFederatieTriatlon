using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IPostareManager
    {
        void Delete(int id);
        void UpdateFericireCresc(int codPostare);
        void UpdateFericireDesc(int codPostare);
        void UpdateTristeteCresc(int codPostare);
        void UpdateTristeteDesc(int codPostare);

        List<PostareByIdUtilizModel> GetPostariIdUtiliz(string idUtiliz);
        List<PostariUtilizatori> GetPostariVazuteDeIdUtiliz(string idUtiliz);
        void Create(PostareCreateModel postare);
        List<PostareComentarii> GetPostariVazuteDeIdUtilizComentarii(string email);
        List<PostareByEmailUtilizModel> GetPostariEmailUtiliz(string email);
        List<PostareModelTotal> GetAllPostari();
    }
}
