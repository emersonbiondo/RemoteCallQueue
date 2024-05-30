using AppServer;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;

namespace AppClient.Presentations.Models
{
    public partial class Client : ObservableObject
    {
        public ClientConfiguration ClientConfiguration { get; set; }

        [ObservableProperty]
        public string message;

        public Client(ClientConfiguration clientConfiguration)
        {
            ClientConfiguration = clientConfiguration;
            Message = string.Empty;
        }

        public void SendMessage()
        {
            using (TcpClient tcpClient = new TcpClient(ClientConfiguration.Server, ClientConfiguration.Port))
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(networkStream))
                    {
                        streamWriter.WriteLine(Message);
                        streamWriter.Flush();
                    }
                }
            }
        }

        public void SendAwait()
        {
            using (TcpClient tcpClient = new TcpClient(ClientConfiguration.Server, ClientConfiguration.Port))
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(networkStream))
                    {
                        streamWriter.WriteLine(ClientConfiguration.TagDefault);
                        streamWriter.Flush();
                    }
                }
            }
        }
    }
}
