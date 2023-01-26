namespace Reformers.ViewModels
{
    internal class MainVM : NotifyPropertyChanged
    {
        IDialogService dialogService;
        MainM mainM;
        public MainVM() 
        {
            mainM = new MainM();
            dialogService = new DefaultDialogService();
        }

        public ObservableCollection<item> Lists { get => mainM.Lists; set => Set(ref mainM.Lists, value); }

        public ObservableCollection<itemGroup> Groups { get => mainM.Groups; set => Set(ref mainM.Groups, value); }


        RelayCommand? _OpenFile;
        public RelayCommand OpenFile => _OpenFile ??= new RelayCommand(OpenFileMethod);

        void OpenFileMethod(object parameter)
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    mainM.Processing(dialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
    }
}
