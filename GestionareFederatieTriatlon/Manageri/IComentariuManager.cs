using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IComentariuManager
    {
        void Create(ComentariuModelCreate comentariu);
        void Delete(int idComentariu);
        List<ComentariiModel> GetComentariiByIdPostare(int idPostare);
        void Update(ComentariuUpdateModel model);
        List<ComentariuCodMesajModel> GetComentariiCodMesaj();
    }
}
