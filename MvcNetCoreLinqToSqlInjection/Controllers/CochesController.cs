using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class CochesController : Controller
    {
        public Coche car;
        public CochesController(Coche car)
        {
            this.car = car;
        }
        public IActionResult Index()
        {
            return View(this.car);
        }
        [HttpPost]
        public IActionResult Index(string accion)
        {
            if (accion.ToLower() == "acelerar")
            {
                this.car.Acelerar();
            }
            else
            {
                this.car.Frenar();
            }
                return View(this.car);
        }
    }
}
