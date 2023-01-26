namespace Reformers.Models
{
    internal class item : NotifyPropertyChanged
    {
        string _name="";
        public string name { get => _name; set => Set(ref _name, value); }

        string _group = "";
        public string group { get => _group; set => Set(ref _group, value); }
    }
}
