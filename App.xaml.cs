namespace Reformers
{
    public partial class App : Application
    {
        App()
        {
            Views.MainWindow mainWindow = new Views.MainWindow() { DataContext=new MainVM() };
            mainWindow.Show();
        }
    }
}
