namespace AppServer
{
    public class ClientConfiguration
    {
        public string Title { get; set; }
        public string TitleMessage { get; set; }
        public string TitleButtonSend { get; set; }
        public string TitleButtonAwait { get; set; }
        public string TitleSucessMessage { get; set; }
        public string TitleSucessAwaitMessage { get; set; }
        public string TitleValidMessage { get; set; }
        public string SucessMessage { get; set; }
        public string SucessAwaitMessage { get; set; }
        public string ValidMessage { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string TagDefault { get; set; }
        public ClientConfiguration()
        {
            Title = string.Empty;
            TitleMessage = string.Empty;
            TitleButtonSend = string.Empty;
            TitleButtonAwait = string.Empty;
            TitleSucessMessage = string.Empty;
            TitleSucessAwaitMessage = string.Empty;
            TitleValidMessage = string.Empty;
            SucessMessage = string.Empty;
            SucessAwaitMessage = string.Empty;
            ValidMessage = string.Empty;
            Server = "127.0.0.1";
            Port = 8888;
            TagDefault = "<BR>";
        }
    }
}
