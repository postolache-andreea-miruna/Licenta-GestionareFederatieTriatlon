using GestionareFederatieTriatlon.Entitati;

namespace GestionareFederatieTriatlon.Repo
{
    public class ChatRepo: IChatRepo
    {
        private readonly GestionareFederatieTriatlonContext db;
        public ChatRepo(GestionareFederatieTriatlonContext db)
        {
            this.db = db;
        }
        public void Update(Utilizator utilizator)
        {
            db.Utilizatori.Update(utilizator);
            db.SaveChanges();
        }

        /* public void CreateChat(Chat chat)
         {
             db.Chaturi.Add(chat);
             db.SaveChanges();
         }*/

        /*  public void CreateConversatie(Conversatie conversatie)
           {
              db.Conversatii.Add(conversatie);
              db.SaveChanges();
            }*/


        public void CreateMesaj(Mesaj mesaj)
        {
            db.Mesaje.Add(mesaj);
            db.SaveChanges();
        }

        /*public IQueryable<Conversatie> GetConversatii()
          {
            var conv = db.Conversatii;
            return conv;
          }*/

        public IQueryable<Mesaj> GetMesaje()
        {
            var mesaje = db.Mesaje;
            return mesaje;
        }

/*        public IQueryable<Chat> GetChaturi()
        {
            var chat = db.Chaturi;
            return chat;
        }*/
    }
}
