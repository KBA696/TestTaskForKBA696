using Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using БибдиотекаMVVM;

namespace Client.ViewModel
{
    public class MainWindow : NotificationObject
    {
        public MainWindow()
        {
            System.Threading.Thread.Sleep(15000);

            GetMimsAsync();
        }

		public async Task GetMimsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync("https://localhost:7158/weatherforecast");

                    var response = await result.Content.ReadAsStringAsync();

                    Messages = JsonConvert.DeserializeObject<List<Message>>(response);

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Recalculation();
                    }));
                }
                catch { }
            }
        }

        /// <summary>
        /// Список сообщений
        /// </summary>
        List<Message> Messages = new List<Message>();

        bool _FilterEnabled=false;
        /// <summary>
        /// Флаг фильтра
        /// </summary>
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                if (value == _FilterEnabled) return;
                _FilterEnabled = value;
                OnPropertyChanged();
                Recalculation();
            }
        }

        DateTime _FromDate = DateTime.Now;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set
            {
                if (value == _FromDate) return;
                //Проверка чтобы дата после не была меньше даты до
                if (_BeforeDate.Date >= value.Date)
                {
                    _FromDate = value;
                }
                else
                {
                    _FromDate = _BeforeDate;
                }

                OnPropertyChanged();
                Recalculation();
            }
        }

        DateTime _BeforeDate = DateTime.Now;
        public DateTime BeforeDate
        {
            get { return _BeforeDate; }
            set
            {
                if (value == _BeforeDate) return;
                //Проверка чтобы дата до не была больше даты после
                if (_FromDate.Date <= value.Date)
                {
                    _BeforeDate = value;
                }
                else
                {
                    _BeforeDate = _FromDate;
                }
                
                OnPropertyChanged();
                Recalculation();
            }
        }

        void Recalculation()
        {
            ListMessages.Clear();
            foreach (var message in Messages)
            {
                if (_FilterEnabled && (FromDate.Date > message.Date.Date || message.Date.Date > BeforeDate.Date))
                {
                    continue;
                }
                ListMessages.Add(new View.MessageControl() { DataContext = new MessageControl(message) });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<object> _ListMessages = new ObservableCollection<object>();
        public ObservableCollection<object> ListMessages
        {
            get { return _ListMessages; }
            set
            {
                if (value == _ListMessages) return;
                _ListMessages = value;
                OnPropertyChanged();
            }
        }

        string _Message;
        /// <summary>
        /// Само сообщение
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set
            {
                if (value == _Message) return;
                _Message = value;
                OnPropertyChanged();
            }
        }

        ICommand _SendingMessage;
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        public ICommand SendingMessage
        {
            get
            {
                return _SendingMessage ?? (_SendingMessage = new RelayCommand<object>(a =>
                {
                    if (!string.IsNullOrEmpty(Message))
                    {
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
                        ManagementObjectCollection collection = searcher.Get();
                        string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
                        // удоление доменной части из имени пользователя
                        var usernameParts = username.Split('\\');
                        username = usernameParts[usernameParts.Length - 1];

                        //Передать на сервер
                        var message = new Message() { Nickname=username, Text=Message, Date=DateTime.Now};
                        GetMimsAsync1(message);
                        
                    }
                }, b => !string.IsNullOrEmpty(Message)));
            }
        }


        public async Task GetMimsAsync1(Message message)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string serialized = JsonConvert.SerializeObject(message);
                    StringContent stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

                    var result = await client.PostAsync("https://localhost:7158/weatherforecast", stringContent);

                    var response = await result.Content.ReadAsStringAsync();

                    Messages.Add(message);
                    ListMessages.Add(new View.MessageControl() { DataContext = new MessageControl(message) });
                    OnPropertyChanged(nameof(ListMessages));
                }
                catch { }
            }
        }
    }
}
