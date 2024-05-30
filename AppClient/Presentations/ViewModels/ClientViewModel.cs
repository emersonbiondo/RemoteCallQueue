using AppClient.Presentations.Models;
using AppClient.Util;
using AppServer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppClient.Presentations.ViewModels
{
    public partial class ClientViewModel : ObservableObject
    {
        public static IAlertService AlertService;

        [ObservableProperty]
        public Client client;

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string titleMessage;

        [ObservableProperty]
        public string titleButtonSend;

        [ObservableProperty]
        public string titleButtonAwait;

        public bool IsMessageValid { get; set; }

        public ClientViewModel(IAlertService alertService, ClientConfiguration clientConfiguration)
        {
            AlertService = alertService;
            Client = new Client(clientConfiguration);
            Title = clientConfiguration.Title;
            TitleMessage = clientConfiguration.TitleMessage;    
            TitleButtonSend = clientConfiguration.TitleButtonSend;
            TitleButtonAwait = clientConfiguration.TitleButtonAwait;
        }

        [RelayCommand]
        public Task Send()
        {
            if (Validate())
            {
                Client.SendMessage();
                if (!string.IsNullOrEmpty(client.ClientConfiguration.SucessMessage))
                {
                    AlertService.ShowAlert(client.ClientConfiguration.TitleSucessMessage, client.ClientConfiguration.SucessMessage);
                }
            }

            return Task.CompletedTask;
        }

        [RelayCommand]
        public Task Await()
        {
            Client.SendAwait();
            if (!string.IsNullOrEmpty(client.ClientConfiguration.SucessAwaitMessage))
            {
                AlertService.ShowAlert(client.ClientConfiguration.TitleSucessAwaitMessage, client.ClientConfiguration.SucessAwaitMessage);
            }

            return Task.CompletedTask;
        }

        public bool Validate()
        {
            var errors = new List<string>();

            if (!IsMessageValid)
            {
                errors.Add(client.ClientConfiguration.ValidMessage);
            }

            if (errors.Count > 0)
            {
                AlertService.ShowAlert(client.ClientConfiguration.TitleValidMessage, string.Join(Environment.NewLine, errors));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
