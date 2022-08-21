using MVVM;
using System;
using System.Windows;
using Test2.Model;

namespace Test2.ViewModel
{
    internal class MainWindow : NotificationObject
    {
        public MainWindow()
        {
            TypeMachines= "Легковой автомобиль";
        }

        /// <summary>
        /// Список типов автомобилей
        /// </summary>
        public string[] TypesMachines { get; set; } = new string[] { "Легковой автомобиль", "Грузовой автомобиль", "Спортивный автомобиль" };

        /// <summary>
        /// Класс выбранного автомобиля (PassengerCar, Truck, SportsCar)
        /// </summary>
        object SelectedCar;

        string _TypeMachines;
        /// <summary>
        /// Выбранный тип автомобиля
        /// </summary>
        public string TypeMachines 
        {
            get { return _TypeMachines; }
            set 
            { 
                _TypeMachines = value;

                switch (_TypeMachines)
                {
                    case "Легковой автомобиль":
                        SelectedCar = new PassengerCar(3);
                        MaxQuantityPassengersText = "Максимальное количество перевозимых пассажиров.";
                        QuantityPassengersText = "Количество перевозимых пассажиров.";
                        MaxQuantityPassengersVisibility1 = Visibility.Visible;
                        MaxCargoWeightVisibility2 = Visibility.Hidden;
                        QuantityPassengersVisibility1 = Visibility.Visible;
                        CargoWeightVisibility2 = Visibility.Hidden;
                        break;
                    case "Грузовой автомобиль":
                        SelectedCar = new Truck(150);
                        MaxQuantityPassengersText = "Максимальный вес груза..";
                        QuantityPassengersText = "Вес груза..";
                        MaxQuantityPassengersVisibility1 = Visibility.Hidden;
                        MaxCargoWeightVisibility2 = Visibility.Visible;
                        QuantityPassengersVisibility1 = Visibility.Hidden;
                        CargoWeightVisibility2 = Visibility.Visible;
                        break;
                    case "Спортивный автомобиль":
                        SelectedCar = new SportsCar();
                        MaxQuantityPassengersText = "";
                        QuantityPassengersText = "";
                        MaxQuantityPassengersVisibility1 = Visibility.Hidden;
                        MaxCargoWeightVisibility2 = Visibility.Hidden;
                        QuantityPassengersVisibility1 = Visibility.Hidden;
                        CargoWeightVisibility2 = Visibility.Hidden;
                        break;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        /// <summary>
        /// Обновить все поля в окне Test2.View.MainWindow
        /// </summary>
        void UpdateAllFields()
        {
            OnPropertyChanged(nameof(MaximumDistance));
            OnPropertyChanged(nameof(TravelTime));
            OnPropertyChanged(nameof(StatusPowerReserve));

            OnPropertyChanged(nameof(MaxQuantityPassengersText));
            OnPropertyChanged(nameof(QuantityPassengersText));
            OnPropertyChanged(nameof(MaxQuantityPassengersVisibility1));
            OnPropertyChanged(nameof(MaxCargoWeightVisibility2));
            OnPropertyChanged(nameof(QuantityPassengersVisibility1));

            OnPropertyChanged(nameof(MaxQuantityPassengers));
            OnPropertyChanged(nameof(MaxCargoWeight));
            OnPropertyChanged(nameof(QuantityPassengers));
            OnPropertyChanged(nameof(CargoWeight));
        }

        /// <summary>
        /// Средний расход топлива
        /// </summary>
        public double AverageFuelConsumption
        {
            get 
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).AverageFuelConsumption;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).AverageFuelConsumption;
                }
                else
                {
                    return ((SportsCar)SelectedCar).AverageFuelConsumption;
                }
            }
            set
            {
                if (SelectedCar is PassengerCar)
                {
                    ((PassengerCar)SelectedCar).AverageFuelConsumption = value;
                }
                else if (SelectedCar is Truck)
                {
                    ((Truck)SelectedCar).AverageFuelConsumption = value;
                }
                else
                {
                    ((SportsCar)SelectedCar).AverageFuelConsumption = value;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        /// <summary>
        /// Объем топливного бака
        /// </summary>
        public double FuelTankCapacity
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).FuelTankCapacity;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).FuelTankCapacity;
                }
                else
                {
                    return ((SportsCar)SelectedCar).FuelTankCapacity;
                }
            }
            set
            {
                if (SelectedCar is PassengerCar)
                {
                    ((PassengerCar)SelectedCar).FuelTankCapacity = value;
                }
                else if (SelectedCar is Truck)
                {
                    ((Truck)SelectedCar).FuelTankCapacity = value;
                }
                else
                {
                    ((SportsCar)SelectedCar).FuelTankCapacity = value;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        /// <summary>
        /// Скорость
        /// </summary>
        public double Speed
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).Speed;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).Speed;
                }
                else
                {
                    return ((SportsCar)SelectedCar).Speed;
                }
            }
            set
            {
                if (SelectedCar is PassengerCar)
                {
                    ((PassengerCar)SelectedCar).Speed = value;
                }
                else if (SelectedCar is Truck)
                {
                    ((Truck)SelectedCar).Speed = value;
                }
                else
                {
                    ((SportsCar)SelectedCar).Speed = value;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        #region Поле с максимальным количеством пасажиров или груза
        public string MaxQuantityPassengersText { get; set; }
        public Visibility MaxQuantityPassengersVisibility1 { get; set; }
        public Visibility MaxCargoWeightVisibility2 { get; set; }

        public int MaxQuantityPassengers
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).MaxQuantityPassengers;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedCar is PassengerCar)
                {
                    ((PassengerCar)SelectedCar).MaxQuantityPassengers = value;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        public double MaxCargoWeight
        {
            get
            {
                if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).MaxCargoWeight;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedCar is Truck)
                {
                    ((Truck)SelectedCar).MaxCargoWeight = value;
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }
        #endregion

        #region Поле с текущим количеством пасажиров или груза
        public string QuantityPassengersText { get; set; }
        Visibility _QuantityPassengersVisibility1;
        public Visibility QuantityPassengersVisibility1
        {
            get { return _QuantityPassengersVisibility1; }
            set
            {
                if (_QuantityPassengersVisibility1 == value) { return; }
                _QuantityPassengersVisibility1 = value;
                OnPropertyChanged();
            }
        }

        Visibility _CargoWeightVisibility2;
        public Visibility CargoWeightVisibility2 
        {
            get { return _CargoWeightVisibility2; }
            set 
            {
                if (_CargoWeightVisibility2 == value) { return; }
                _CargoWeightVisibility2 = value;
                OnPropertyChanged();
            } 
        }

        public int QuantityPassengers
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).QuantityPassengers;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedCar is PassengerCar)
                {
                    try
                    {
                        ((PassengerCar)SelectedCar).QuantityPassengers = value;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }

        public double CargoWeight
        {
            get
            {
                if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).CargoWeight;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (SelectedCar is Truck)
                {
                    try
                    {
                        ((Truck)SelectedCar).CargoWeight = value;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                OnPropertyChanged();
                UpdateAllFields();
            }
        }
        #endregion

        /// <summary>
        /// Заполняем поле Километраж 
        /// </summary>
        public double MaximumDistance
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).MaximumDistance;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).MaximumDistance;
                }
                else
                {
                    return ((SportsCar)SelectedCar).MaximumDistance;
                }
            }
        }

        /// <summary>
        /// Заполняем поле Запас хода
        /// </summary>
        public double TravelTime
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).TravelTime;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).TravelTime;
                }
                else
                {
                    return ((SportsCar)SelectedCar).TravelTime;
                }
            }
        }

        /// <summary>
        /// Заполняем поле Время в пути (ч)
        /// </summary>
        public double StatusPowerReserve
        {
            get
            {
                if (SelectedCar is PassengerCar)
                {
                    return ((PassengerCar)SelectedCar).StatusPowerReserve;
                }
                else if (SelectedCar is Truck)
                {
                    return ((Truck)SelectedCar).StatusPowerReserve;
                }
                else
                {
                    return ((SportsCar)SelectedCar).StatusPowerReserve;
                }
            }
        }
    }
}
