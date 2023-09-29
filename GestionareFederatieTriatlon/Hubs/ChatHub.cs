using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;

namespace GestionareFederatieTriatlon.Hubs
{
    public class ChatHub: Hub //cele prin care se face comunicarea cu clientul
    {
        private readonly UserManager<Utilizator> userManager;
        private readonly IChatManager manager;
        public ChatHub(UserManager<Utilizator> userManager, IChatManager manager)
        {
            this.userManager = userManager;
            this.manager = manager;
        }
        private static List<string> groups = new List<string>();
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Chat");
            await Clients.Caller.SendAsync("ConnectedUser");
          
        }
       
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Chat");

            var utilizator = userManager.Users.Where(u => u.codConexiune == Context.ConnectionId).FirstOrDefault();
            manager.UpdateCodConexiune(utilizator.Id, null);
            manager.UpdateDisponibilitate(utilizator.Id, false);

            await UtilizatoriOnline();
            await UtilizatoriOffline();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task ConnectionIdForUser(string email)
        {
            var utilizator = userManager.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            manager.UpdateCodConexiune(utilizator.Id, Context.ConnectionId);

            manager.UpdateDisponibilitate(utilizator.Id, true); 

            await UtilizatoriOnline();
            await UtilizatoriOffline();
         
        }

        public async Task CreatePrivateChat(ConversatieModel message)
        {
            var codUtilizatorFrom = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault();
            var codUtilizatorTo = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault();

            var disponibilitateFrom = manager.GetDisponibilitate(codUtilizatorFrom);
            var disponibilitateTo = manager.GetDisponibilitate(codUtilizatorTo);

            if (disponibilitateFrom == true && disponibilitateTo == false)
            {
                manager.UpdateCitireMesajSpreUserNelogat(codUtilizatorFrom, codUtilizatorTo, true);
                string privateGroupName = GetPrivateGroupName(message.codUtilizator, "");//de fapt sunt adresele de mail nu codurile
                await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);

                var newMesaj = new ConversatieCreateModel
                {
                    codUtilizator = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault(),
                    codUtilizator2 = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault(),
                    mesajConversatie = message.mesajConversatie,
                    citireMesaj = false
                };

                manager.CreateMesaj(newMesaj);
            }

            if (disponibilitateFrom == true && disponibilitateTo == true)
            {
                manager.UpdateDisponibilitate(codUtilizatorFrom, false);
                manager.UpdateDisponibilitate(codUtilizatorTo,false);
                manager.UpdateCitireMesaj(codUtilizatorFrom, codUtilizatorTo, true);
                await UtilizatoriOnline();
                string privateGroupName = GetPrivateGroupName(message.codUtilizator, message.codUtilizator2);//de fapt sunt adresele de mail nu codurile

                groups.Add(privateGroupName);
                
                await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
                
                var connectionIdUtiliz2 = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.codConexiune).FirstOrDefault();
                await Groups.AddToGroupAsync(connectionIdUtiliz2, privateGroupName);
                //noi aici trebuie sa avem metoda de a crea in baza de data un chat
  
                var newMesaj = new ConversatieCreateModel
                {
                    codUtilizator = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault(),
                    codUtilizator2 = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault(),
                    mesajConversatie = message.mesajConversatie,
                    citireMesaj = true
                };

                manager.CreateMesaj(newMesaj);

                await Clients.Client(connectionIdUtiliz2).SendAsync("OpenPrivateChat", message);
                
            }

        }

        public async Task ReceivePrivateMessage(ConversatieModel message) 
        {

            var codUtilizatorFrom = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault();
            var codUtilizatorTo = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault();

            var disponibilitateFrom = manager.GetDisponibilitate(codUtilizatorFrom);
            var disponibilitateTo = manager.GetDisponibilitate(codUtilizatorTo);

            if (disponibilitateFrom == true && disponibilitateTo == false)
            {
                string privateGroupNam = GetPrivateGroupName(message.codUtilizator, "");
                var newMesaj = new ConversatieCreateModel
                {
                    codUtilizator = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault(),
                    codUtilizator2 = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault(),
                    mesajConversatie = message.mesajConversatie,
                    citireMesaj = false
                };

                manager.CreateMesaj(newMesaj);
                await Clients.Group(privateGroupNam).SendAsync("NewPrivateMessage", message);
            }


            if ((disponibilitateFrom == false && disponibilitateTo == false) || (disponibilitateFrom == true && disponibilitateTo == true)) 
            {
                string privateGroupName = GetPrivateGroupName(message.codUtilizator, message.codUtilizator2);

                var esteGrup = groups.Contains(privateGroupName);//verificam daca acesta este un grup din sesiune
                if (esteGrup == false)
                {


                    await Clients.Group(GetPrivateGroupName(message.codUtilizator, "")).SendAsync("NewPrivateMessage", message);///pentru a arata mesajul trimis cand utilizatorul cu care vb devine disponibil
                    await CreatePrivateChat(message);
                }
                else
                {
                    var newMesaj = new ConversatieCreateModel
                    {
                        codUtilizator = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator)).Select(u => u.Id).FirstOrDefault(),
                        codUtilizator2 = userManager.Users.Where(u => u.Email.Equals(message.codUtilizator2)).Select(u => u.Id).FirstOrDefault(),
                        mesajConversatie = message.mesajConversatie,
                        citireMesaj = true
                    };

                    manager.CreateMesaj(newMesaj);
                    await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
                }                              
            }               
        }

        public async Task ClosePrivateChat(string emailUtilizator, string emailUtilizator2)//se primeste emailul
        {
            
            var codUtilizatorFrom = userManager.Users.Where(u => u.Email.Equals(emailUtilizator)).Select(u => u.Id).FirstOrDefault();
            var codUtilizatorTo = userManager.Users.Where(u => u.Email.Equals(emailUtilizator2)).Select(u => u.Id).FirstOrDefault();

            var disponibilitateFrom = manager.GetDisponibilitate(codUtilizatorFrom);
            var disponibilitateTo = manager.GetDisponibilitate(codUtilizatorTo);

            if(disponibilitateFrom == false && disponibilitateTo == false)
            {
                manager.UpdateDisponibilitate(codUtilizatorFrom, true);
                manager.UpdateDisponibilitate(codUtilizatorTo, true);
                await UtilizatoriOnline();
                await UtilizatoriAndUnreadMessages(emailUtilizator);


                string privateGroupName = GetPrivateGroupName(emailUtilizator, emailUtilizator2);

                groups.Remove(privateGroupName);

                await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
                var connectionIdUtiliz2 = userManager.Users.Where(u => u.Email.Equals(emailUtilizator2)).Select(u => u.codConexiune).FirstOrDefault();
                await Groups.RemoveFromGroupAsync(connectionIdUtiliz2, privateGroupName);
            }
            if(disponibilitateFrom == true && disponibilitateTo == false)
            {

                await UtilizatoriOnline();
                await UtilizatoriAndUnreadMessages(emailUtilizator);
                string privateGroupName = GetPrivateGroupName(emailUtilizator, "");
                await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
            }
                        
        }

        private async Task UtilizatoriOnline() 
        {
            var utilizatori = manager.UsersOnline();
            await Clients.Groups("Chat").SendAsync("OnlineUsers", utilizatori);
        }
        private string GetPrivateGroupName(string codUtilizator, string codUtilizator2) 
        {
            var stringCompare = string.CompareOrdinal(codUtilizator, codUtilizator2) < 0;
            return stringCompare ? $"{codUtilizator}-{codUtilizator2}" : $"{codUtilizator2}-{codUtilizator}";
        }

        private async Task UtilizatoriOffline()
        {
            var utilizatoriOff = manager.UsersOffline();
            await Clients.Groups("Chat").SendAsync("OfflineUsers", utilizatoriOff);
        }

        public async Task UtilizatoriAndUnreadMessages(string emailUtilizatorConectat)
        {
            var utilizOnMessages = manager.MessagesUnreadFromUsersByEmail(emailUtilizatorConectat);
            var connectionIdUtiliz = userManager.Users.Where(u => u.Email.Equals(emailUtilizatorConectat)).Select(u => u.codConexiune).FirstOrDefault();
            await Clients.Client(connectionIdUtiliz).SendAsync("UsersMessagesNr", utilizOnMessages);
        }

    }
}
