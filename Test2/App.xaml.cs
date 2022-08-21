using System.Windows;

namespace Test2
{
    public partial class App : Application
    {
        public App()
        {
            var Window = new View.MainWindow() { DataContext = new ViewModel.MainWindow() };
            Window.Show();
        }
    }
}
