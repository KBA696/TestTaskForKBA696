using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using ТестовоеЗадание1;
using ТестовоеЗадание1.Model;

namespace EmptyApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница без перечня товаров
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Переменная для js максимальная цена из всех продуктов
            ViewData["max_price"] = Startup.max_price;
            //Переменная для js максимальный рейтинг из всех продуктов
            ViewData["max_rating"] = Startup.max_rating;

            ViewBag.Countries = Startup.Category;
            return View();
        }

        /// <summary>
        /// Список товаров для главной страницы
        /// </summary>
        /// <returns></returns>
        public IActionResult Time()
        {
            var categoryS = Request.Query["category"];
            var min_priceS = Request.Query["min_price"];
            var max_priceS = Request.Query["max_price"];
            var min_ratingS = Request.Query["min_rating"];
            var max_ratingS = Request.Query["max_rating"];
            var sortS = Request.Query["sort"];

            int.TryParse(categoryS, out int category);
            int.TryParse(min_priceS, out int min_price);
            int.TryParse(max_priceS, out int max_price);
            int.TryParse(min_ratingS, out int min_rating);
            int.TryParse(max_ratingS, out int max_rating);
            int.TryParse(sortS, out int sort);

            var producti = Startup.products.Where(x => x.Category == category && x.Price >= min_price && x.Price <= max_price && x.Category >= min_rating && x.Category <= max_rating);

            switch (sort)
            {
                case 1:
                    producti = producti.OrderByDescending(x => x.Price);
                    break;
                case 2:
                    producti = producti.OrderBy(x => x.Rating);
                    break;
                case 3:
                    producti = producti.OrderByDescending(x => x.Rating);
                    break;
                default:
                    producti = producti.OrderBy(x => x.Price);
                    break;
            }

            ViewBag.Countries = producti;

            return View();
        }

        /// <summary>
        /// Страница админ панели без авторизации и добавления товара
        /// </summary>
        /// <returns></returns>
        public IActionResult Admin()
        {
            return View();
        }

        string login = "admin";
        string pas = "125";
        /// <summary>
        /// Авторизация в админ панель. Если удачно открывается панель добавления товара
        /// </summary>
        /// <returns></returns>
        public IActionResult Admi()
        {


            var loginS = Request.Query["login"];
            var passwordS = Request.Query["pas"];
            bool avtor = false;
            if (!string.IsNullOrEmpty(loginS) && !string.IsNullOrEmpty(passwordS))
            {
                if (loginS == login && passwordS == pas)
                {
                    Response.Cookies.Append("name", login);
                    avtor = true;
                }
            }
            ViewBag.Countries = Startup.Category;
            ViewBag.Prov = Request.Cookies.ContainsKey("name") || avtor;

            return View();
        }

        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Addproduct()
        {
            if (Request.Cookies.ContainsKey("name"))
            {
                string adr = "";
                var NameS = Request.Form["Name"];
                var CategoryS = Request.Form["Category"];
                var DescriptionS = Request.Form["Description"];
                var PriceS = Request.Form["Price"];
                int.TryParse(CategoryS, out int category);
                double.TryParse(PriceS, out double price);

                if (!string.IsNullOrEmpty(NameS) && !string.IsNullOrEmpty(DescriptionS) && price > 0)
                {
                    try
                    {
                        var files = Request.Form.Files;
                        foreach (var uploadedFile in files)
                        {
                            if (uploadedFile.Length > 0)
                            {
                                Directory.CreateDirectory(@".\wwwroot\images");
                                adr = @"\images\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + uploadedFile.FileName;

                                using (FileStream fileStream = System.IO.File.Create(@".\wwwroot" + adr, (int)uploadedFile.OpenReadStream().Length))
                                {
                                    // Размещает массив общим размером равным размеру потока
                                    // Могут быть трудности с выделением памяти для больших объемов
                                    byte[] data = new byte[uploadedFile.OpenReadStream().Length];

                                    uploadedFile.OpenReadStream().Read(data, 0, (int)data.Length);
                                    fileStream.Write(data, 0, data.Length);
                                }
                            }
                        }
                    }
                    catch (Exception e) { }
                    Startup.products.Add(new Product() { Name = NameS, Category = category, Description = DescriptionS, Price = price, Rating = 0, Picture = "." + adr });
                }
            }

            ViewBag.Countries = Startup.Category;
            return View();
        }
    }
}