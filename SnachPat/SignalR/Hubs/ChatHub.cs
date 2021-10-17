using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using SnachPat.Services;

namespace PhotoWebPage.SignalR
{
    public class ChatHub : Hub
    {
        //#TODO remove _photoservice
        private readonly PhotoService _photoService;
        public ChatHub(PhotoService photoService)
        {
            _photoService = photoService;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        //Wysyłam zdjęcie i jednocześnie SendImage bierze path do zdjęcia i wysyła na chat
        public async Task SendMessageAndPath(string user, string message, string path)
        {
            await Clients.All.SendAsync("ReceiveMessageAndPath", user, message, path);
        }

    }
}