using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ТестовоеЗадание1.Model
{
    public class Product
    {
        /// <summary>
        /// название товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Картинка товара
        /// </summary>
        public string Picture { get; set; }
    }
}
