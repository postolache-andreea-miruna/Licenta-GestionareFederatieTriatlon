using GestionareFederatieTriatlon.Modele;

namespace GestionareFederatieTriatlon.Manageri
{
    public interface IVideoclipManager
    {
        void Create(VideoModelCreate videoCreate);
        void Update(VideoModelUpdate videoModelUpdate);
        List<VideoModel> GetVideouri();
        CodYoutubeVideoModel GetCodVideoCompetitie(int codCompetitie);
    }
}
