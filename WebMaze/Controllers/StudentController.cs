using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMaze.Models;

namespace WebMaze.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            var models = new List<StudentViewModel>();

            for (int i = 0; i < 5; i++)
            {
                var model = new StudentViewModel();
                model.Second = DateTime.Now.Second + i * 2;
                model.CurentUserName = "Ivan" + i;
                model.DayOfWeek = DateTime.Now.DayOfWeek.ToString();
                models.Add(model);
            }

            return View(models);
        }

        public IActionResult Meiramov()
        {
            var models = new List<GirlViewModel>();

            var meiViewModel = new GirlViewModel();
            meiViewModel.Name = "mei";
            meiViewModel.Hegith  = 150;
            meiViewModel.Url = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a2/Mei_Overwatch.png/220px-Mei_Overwatch.png";
            models.Add(meiViewModel);

            var diva = new GirlViewModel();
            diva.Name = "diva";
            diva.Hegith = 150;
            diva.Url = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a2/Mei_Overwatch.png/220px-Mei_Overwatch.png";
            models.Add(diva);

            return View(models);
        }

        public IActionResult Mochalkin()
        {
            var hookahs = new List<HookahViewModel>();

            var desvallViewModel = new HookahViewModel();
            desvallViewModel.Name = "Desvall";
            desvallViewModel.Price = 40000;
            desvallViewModel.Material = "Stainless steel, inlaid with gold, chromium";
            desvallViewModel.ManufacturerCountry = "Sweden";
            desvallViewModel.Discription = "Кальяны этой компании выполняютя вручную и по индивидуальным заказам. " +
                                           "Есть модели из золота, с камнями Сваровски, хромированные. " +
                                           "Есть кальян стоимостью 100 000$, который был сделан по индивидуальному заказу для марки Bugatti. " +
                                           "Именно после презентации этого кальяна, компания обрела известность и популярность.";
            desvallViewModel.Url = "/image/Mochalkin/Desvall.jpg";
            hookahs.Add(desvallViewModel);

            var aurentumViewModel = new HookahViewModel();
            aurentumViewModel.Name = "Aurentum";
            aurentumViewModel.Price = 1000000;
            aurentumViewModel.Material = "Gold, rhrodium, silver";
            aurentumViewModel.ManufacturerCountry = "Switzerland";
            aurentumViewModel.Discription = "На данный момент самым дорогим кальяном является изобретение марки " +
                                            "Aurentum Switzerland. Он оценивается в 1 миллион американских долларов. " +
                                            "Его украшают рубины и брильянты. Для производства трубки, точнее ее покрытия, " +
                                            "применили родий и серебро. Изделие декорировано изысканными платиновыми фигурами " +
                                            "жучков-скарабеев. При производстве кальяна использовали 12 килограммов 24-каратного " +
                                            "золота. Также вы можете заказать собственный дизайн кальяна, но эта услуга платная, " +
                                            "и начинается от 70 тысяч долларов.";
            aurentumViewModel.Url = "/image/Mochalkin/Aurentum.jpg";
            hookahs.Add(aurentumViewModel);

            return View(hookahs);
        }
    }
}
