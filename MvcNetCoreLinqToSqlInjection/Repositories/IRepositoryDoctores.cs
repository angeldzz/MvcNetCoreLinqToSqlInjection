using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();
        Task CreateDoctorAsync(int idDoctor, string apellido, string especialidad,
                                        int salario, int idHospital);
        Task DeleteDoctorAsync(int iddoctor);
        Task UpdateDoctorAsync(int idDoctor, string apellido, string especialidad,
                                        int salario, int idHospital);
        List<Doctor> GetDoctoresEspecialidad(string especialidad);
    }
}
