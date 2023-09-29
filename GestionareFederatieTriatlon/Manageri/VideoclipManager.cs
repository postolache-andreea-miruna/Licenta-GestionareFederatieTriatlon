using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;

namespace GestionareFederatieTriatlon.Manageri
{
    public class VideoclipManager: IVideoclipManager
    {
        private readonly IVideoclipRepo videoRepo;
        public VideoclipManager(IVideoclipRepo videoRepo)
        {
            this.videoRepo = videoRepo;
        }

        public void Create(VideoModelCreate videoCreate)
        {
            var newVideo = new Videoclip
            {
                urlVideo = videoCreate.urlVideo,
                codYoutubeVideo = videoCreate.codYoutubeVideo,
                codCompetitie = videoCreate.codCompetitie
            };
            videoRepo.Create(newVideo);
        }

        public void Update(VideoModelUpdate videoModelUpdate)
        {
            var video = videoRepo.GetVideoclipuri()
                .FirstOrDefault(x => x.codVideo == videoModelUpdate.codVideo);
            if (video == null)
                return;
            video.urlVideo = videoModelUpdate.urlVideo;
            video.codYoutubeVideo = videoModelUpdate.codYoutubeVideo;
            videoRepo.Update(video);
        }

        public List<VideoModel> GetVideouri()
        {
            var videoclipuri = videoRepo.GetVideoclipuri();
            if (videoclipuri == null)
                return new List<VideoModel>();
            var videoModel = videoclipuri
                .Select(v => new VideoModel
                {
                    urlVideo= v.urlVideo,

                    numeCompetitie = v.Competitie.numeCompetitie,
                    dataStart = v.Competitie.dataStart,
                    dataFinal = v.Competitie.dataFinal,
                    codVideo = v.codVideo,
                    codYoutubeVideo = v.codYoutubeVideo

                })
                .OrderByDescending(v => v.dataStart)
                .ToList();
            return videoModel;
        }

        public CodYoutubeVideoModel GetCodVideoCompetitie(int codCompetitie)
        {
            var codYTvideoclip = videoRepo.GetVideoclipuri()
                .Where(v => v.codCompetitie == codCompetitie)

                .Select(v => new CodYoutubeVideoModel
                {
                   codYoutubeVideo = v.codYoutubeVideo,

                })
                .FirstOrDefault();
            return codYTvideoclip;
        }
    }
}
