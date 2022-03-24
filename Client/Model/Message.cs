using System;

namespace Client.Model
{
    [Serializable]
    public class Message
    {
        public string Nickname { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
