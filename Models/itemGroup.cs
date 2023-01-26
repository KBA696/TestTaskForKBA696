namespace Reformers.Models
{
    internal class itemGroup : NotifyPropertyChanged
    {
        public itemGroup(string name)
        {
            cityName = name;
            streetList = new List<itemGroup>();
        }

        private string cityName;
        public string CityName { get => cityName; set => Set(ref cityName, value); }

        private List<itemGroup> streetList;
        public List<itemGroup> StreetList { get => streetList; set => Set(ref streetList, value); }
    }
}
