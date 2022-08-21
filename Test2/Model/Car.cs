namespace Test2.Model
{
    /// <summary>
    /// Класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, объем топливного бака, скорость.
    /// </summary>
    internal class Car
    {
        public Car(string TS)
        {
            this.TS = TS;
        }
        /// <summary>
        /// ТС
        /// </summary>
        public readonly string TS = nameof(Car);

        /// <summary>
        /// Средний расход топлива 1л=
        /// </summary>
        public double AverageFuelConsumption=10;

        /// <summary>
        /// Объем топливного бака
        /// </summary>
        public double FuelTankCapacity=100;

        /// <summary>
        /// Скорость
        /// </summary>
        public double Speed=100;

        /// <summary>
        /// Метод, с помощью которого можно вычислить сколько автомобиль может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент.
        /// </summary>
        public double MaximumDistance
        {
            get
            {
                return FuelTankCapacity * AverageFuelConsumption;
            }
        }

        /// <summary>
        /// Метод для отображения текущей информации о состоянии запаса хода.
        /// </summary>
        public virtual double StatusPowerReserve 
        {
            get
            {
                return MaximumDistance;
            }
        }


        /// <summary>
        /// Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько автомобиль его преодолеет.?????
        /// </summary>
        public double TravelTime 
        {
            get
            {
                return MaximumDistance / Speed;
            }
        }
    }
}
