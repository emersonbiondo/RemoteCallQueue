using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;

namespace AppServer.Presentations.Models
{
    public partial class Server: ObservableObject
    {
        private TcpListener tcpListener;
        private CancellationTokenSource? cancellationTokenSource;
        private ServerConfiguration serverConfiguration;
        private SpeechOptions speechOptions;
        private string lastMessage;

        [ObservableProperty]
        public ObservableCollection<string> messages;

        [ObservableProperty]
        public string message;

        [ObservableProperty]
        public string title2;

        [ObservableProperty]
        public string title3;

        public Server(ServerConfiguration serverConfiguration)
        {
            this.serverConfiguration = serverConfiguration;
            tcpListener = new TcpListener(IPAddress.Any, serverConfiguration.Port);
            Messages = new ObservableCollection<string>();
            Title2 = string.Empty;
            Title3 = string.Empty;
            Message = serverConfiguration.MessageDefault;
            lastMessage = string.Empty;
            Start();
            ConfigureSpeech();
        }

        public async void ConfigureSpeech()
        {
            IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

            speechOptions = new SpeechOptions()
            {
                Pitch = serverConfiguration.Pitch,
                Volume = serverConfiguration.Volume,
                Locale = locales.FirstOrDefault()
            };
        }

        public void Start()
        {
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var cancel = false;

                    tcpListener.Start();

                    while (!cancel)
                    {
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();
                        Thread clientThread = new Thread(() => HandleClient(tcpClient));
                        clientThread.Start();
                    }

                    if (cancel)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                }, cancellationToken);
            }
        }

        public void Stop()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    var streamReader = new StreamReader(networkStream);

                    if (streamReader != null)
                    {
                        var message = streamReader.ReadLine();

                        if (message != null)
                        {
                            if (!string.IsNullOrEmpty(message))
                            {
                                if (message.Equals(serverConfiguration.TagDefault))
                                {
                                    Message = serverConfiguration.MessageDefault;
                                    Title2 = string.Empty;
                                    Title3 = string.Empty;
                                }
                                else
                                {
                                    if (!Message.Equals(serverConfiguration.MessageDefault) && !Message.Equals(message))
                                    {
                                        if (Messages.Count > 0)
                                        {
                                            lastMessage = Messages.Last();
                                        }
                                        else
                                        {
                                            lastMessage = message;
                                        }

                                        while (Messages.Count >= serverConfiguration.LimitMessages)
                                        {
                                            Messages.RemoveAt(0);
                                        }
                                        Messages.Add(Message);
                                    }
                                    TextToSpeech.Default.SpeakAsync(message, speechOptions);
                                    Message = message; 
                                    Title2 = serverConfiguration.Title2;
                                    Title3 = serverConfiguration.Title3;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
