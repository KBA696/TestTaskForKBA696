using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            View.MainWindow mainWindow = new View.MainWindow();
            mainWindow.DataContext = new ViewModel.MainWindow();
            mainWindow.Show();
        }
    }
}
