namespace CommonViews
{
    #region Делегаты для методов WPF команд
    public delegate void ExecuteHandler(object parameter);
    public delegate bool CanExecuteHandler(object parameter);
    #endregion

    #region Класс команд - RelayCommand
    /// <summary>Класс реализующий <see cref="ICommand"/>.<br/>
    /// Реализация взята из <see href="https://www.cyberforum.ru/wpf-silverlight/thread2390714-page4.html#post13535649"/> и дополнена конструктором для методов без параметра.</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler canExecute;
        private readonly ExecuteHandler execute;
        private readonly EventHandler requerySuggested;

        /// <summary>Событие извещающее об изменении состояния команды.</summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>Конструктор команды.</summary>
        /// <param name="execute">Выполняемый метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null) : this()
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;

            requerySuggested = (o, e) => Invalidate();
            CommandManager.RequerySuggested += requerySuggested;
        }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler, CanExecuteHandler)"/>
        public RelayCommand(Action execute, Func<bool> canExecute = null) : this
                (
                      p => execute(),
                      p => canExecute?.Invoke() ?? true
                )
        { }

        private RelayCommand() => dispatcher = Application.Current.Dispatcher;

        private readonly Dispatcher dispatcher;

        /// <summary>Метод, подымающий событие <see cref="CanExecuteChanged"/>.</summary>
        public void RaiseCanExecuteChanged()
        {
            if (dispatcher.CheckAccess())
                Invalidate();
            else
                dispatcher.BeginInvoke(Invalidate);
        }
        private void Invalidate() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Вызов метода, возвращающего состояние команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено.</returns>
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        /// <summary>Вызов выполняющего метода команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter) => execute?.Invoke(parameter);
    }
    #endregion

    #region Делегаты для методов WPF команд
    public delegate void ExecuteHandler<T>(T parameter);
    public delegate bool CanExecuteHandler<T>(T parameter);
    #endregion

    /// <summary>Реализация RelayCommand для методов с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра методов.</typeparam>
    public class RelayCommand<T> : RelayCommand
    {
        /// <inheritdoc cref="RelayCommand(ExecuteHandler, CanExecuteHandler)"/>
        public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute = null) : base
            (
                  p =>
                  {
                      if (p is T t)
                          execute(t);
                  },
                  p => (p is T t) && (canExecute?.Invoke(t) ?? true)
            )
        { }
    }
}
