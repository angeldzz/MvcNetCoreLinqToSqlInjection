using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        public RepositoryDoctoresSQLServer repo;
        //Recibimos nuestro repositorio
        public DoctoresController(RepositoryDoctoresSQLServer repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            await this.repo.CreateDoctor(doctor.IdDoctor,doctor.Apellido,doctor.Especialidad,doctor.Salario,doctor.IdHospital);
            return RedirectToAction("Index");
        }
    }
}
