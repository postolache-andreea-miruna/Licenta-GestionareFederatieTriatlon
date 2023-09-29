using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public interface IChatRepo
    {
        void Update(Utilizator utilizator);
        void CreateMesaj(Mesaj mesaj);
        IQueryable<Mesaj> GetMesaje();
        //void CreateChat(Chat chat);
        //void CreateConversatie(Conversatie conversatie);
        //IQueryable<Conversatie> GetConversatii();
        //IQueryable<Chat> GetChaturi();
    }
}
