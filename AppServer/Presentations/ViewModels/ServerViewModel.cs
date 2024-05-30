using AppServer.Presentations.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppServer.Presentations.ViewModels
{
    public partial class ServerViewModel: ObservableObject
    {
        [ObservableProperty]
        public Server server;

        [ObservableProperty]
        public string title1; 
        
        [ObservableProperty]
        public string title4;

        [ObservableProperty]
        public string background;

        [ObservableProperty]
        public int fontTitle2;

        [ObservableProperty]
        public int fontTitle3;

        [ObservableProperty]
        public int fontTitle4;

        [ObservableProperty]
        public int fontMessage;

        [ObservableProperty]
        public int fontOldMessage;

        public ServerViewModel(ServerConfiguration serverConfiguration)
        {
            Server = new Server(serverConfiguration);
            Title1 = serverConfiguration.Title1;
            Title4 = serverConfiguration.Title4;
            Background = serverConfiguration.Background;
            FontTitle2 = serverConfiguration.FontTitle2;
            FontTitle3 = serverConfiguration.FontTitle3;
            FontTitle4 = serverConfiguration.FontTitle4;
            FontMessage = serverConfiguration.FontMessage;
            FontOldMessage = serverConfiguration.FontOldMessage;
        }
    }
}
