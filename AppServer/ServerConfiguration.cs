namespace AppServer
{
    public class ServerConfiguration
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Title4 { get; set; }
        public string Background { get; set; }
        public int Port { get; set; }
        public string MessageDefault { get; set; }
        public string TagDefault { get; set; }
        public int LimitMessages { get; set; }
        public int FontTitle2 { get; set; }
        public int FontTitle3 { get; set; }
        public int FontTitle4 { get; set; }
        public int FontMessage { get; set; }
        public int FontOldMessage { get; set; }
        public float Pitch { get; set; }
        public float Volume { get; set; }

        public ServerConfiguration()
        {
            Title1 = string.Empty;
            Title2 = string.Empty;
            Title3 = string.Empty;
            Title4 = string.Empty;
            Background = string.Empty;
            Port = 8888;
            MessageDefault = string.Empty;
            TagDefault = "<BR>";
            LimitMessages = 3;
            FontTitle2 = 30;
            FontTitle3 = 30;
            FontTitle4 = 30;
            FontMessage = 60;
            FontOldMessage = 20;
            Pitch = 1.5f;
            Volume = 0.75f;
        }
    }
}
