using System;

namespace Test2.Model
{
    /// <summary>
    /// Легковой автомобиль
    /// </summary>
    internal class PassengerCar : Car
    {
        public PassengerCar(int MaxQuantityPassengers) : base("Легковой автомобиль")
        {
            this.MaxQuantityPassengers = MaxQuantityPassengers;
        }

        /// <summary>
        /// Максимальное количество перевозимых пассажиров.
        /// </summary>
        public int MaxQuantityPassengers;

        int _QuantityPassengers;
        /// <summary>
        /// Количество перевозимых пассажиров.
        /// </summary>
        public int QuantityPassengers
        {
            get { return _QuantityPassengers; }
            set
            {
                if (value > MaxQuantityPassengers)
                {
                    throw new Exception("Столько пассажиров не влезит");
                }
                if (value < 0)
                {
                    throw new Exception("Количество пассажиров не может быть отрицательным");
                }
                _QuantityPassengers = value;
            }
        }

        /// <summary>
        /// Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров.
        /// Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%.
        /// </summary>
        public override double StatusPowerReserve
        {
            get
            {
                return MaximumDistance / 100 * (100 - QuantityPassengers * 6);
            }
        }
    }
}
