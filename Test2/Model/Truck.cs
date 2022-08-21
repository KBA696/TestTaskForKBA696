using System;

namespace Test2.Model
{
    /// <summary>
    /// Грузовой автомобиль
    /// </summary>
    internal class Truck : Car
    {
        public Truck(double MaxCargoWeight) : base("Грузовой автомобиль") 
        { 
            this.MaxCargoWeight = MaxCargoWeight; 
        }

        /// <summary>
        /// Максимальное количество перевозимых пассажиров.
        /// </summary>
        public double MaxCargoWeight;

        double _CargoWeight;
        /// <summary>
        /// Вес груза.
        /// </summary>
        public double CargoWeight
        {
            get { return _CargoWeight; }
            set
            {
                if (value > MaxCargoWeight)
                {
                    throw new Exception("Столько не влезет в грузовик");
                }
                if (value < 0)
                {
                    throw new Exception("Вес груза не может быть отрицательным");
                }
                _CargoWeight = value;
            }
        }

        /// <summary>
        /// Метод для отображения текущей информации о состоянии запаса хода в зависимости от груза
        /// Каждые дополнительные 200кг веса уменьшают запас хода на 4%.
        /// </summary>
        public override double StatusPowerReserve
        {
            get
            {
                var fd = CargoWeight / 200f;
                return MaximumDistance / 100 * (100 - (int)fd * 4);
            }
        }
    }
}
