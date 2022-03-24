using БибдиотекаMVVM;

namespace Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageControl : NotificationObject
    {
        public MessageControl(Message message)
        {
            Text = message.Text;
            NicknameDate = message.Nickname + " " + message.Date.ToString();
        }

        string _NicknameDate;
        /// <summary>
        /// Никнейм и дата
        /// </summary>
        public string NicknameDate
        {
            get { return _NicknameDate; }
            set
            {
                if (value == _NicknameDate) return;
                _NicknameDate = value;
                OnPropertyChanged();
            }
        }

        string _Text;
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value == _Text) return;
                _Text = value;
                OnPropertyChanged();
            }
        }
    }
}
