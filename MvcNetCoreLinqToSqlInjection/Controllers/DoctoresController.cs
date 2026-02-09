using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        //public RepositoryDoctoresSQLServer repo;
        public IRepositoryDoctores repo;
        //Recibimos nuestro repositorio
        public DoctoresController(IRepositoryDoctores repo)
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
            await this.repo.CreateDoctorAsync(doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.IdHospital);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int iddoctor)
        {
            await this.repo.DeleteDoctorAsync(iddoctor);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int iddoctor)
        {
            Doctor doctor = this.repo.GetDoctores().FirstOrDefault(d => d.IdDoctor == iddoctor);
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.IdHospital);
            return RedirectToAction("Index");
        }
        public IActionResult BuscarPorEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }
        [HttpPost]
        public IActionResult BuscarPorEspecialidad(string especialidad)
        {
            List<Doctor> doctores = this.repo.GetDoctoresEspecialidad(especialidad);
            return View(doctores);
        }
    }
}
