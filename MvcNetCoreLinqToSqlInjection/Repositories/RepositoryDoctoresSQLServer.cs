using Microsoft.Data.SqlClient;
using MvcNetCoreLinqToSqlInjection.Models;
using System.Data;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tablaDoctor;
        public RepositoryDoctoresSQLServer()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM DOCTOR", this.cn);
            this.tablaDoctor = new DataTable();
            ad.Fill(this.tablaDoctor);
        }
        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
                doctor.Apellido = row.Field<string>("APELLIDO");
                doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
                doctor.Salario = row.Field<int>("SALARIO");
                doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doctor);
            }
        return doctores;
        }
        public async Task CreateDoctor(int idDoctor, string apellido, string especialidad,
                                        int salario, int idHospital)
        {
            string sql = "INSERT INTO DOCTOR VALUES (@IDHOSPITAL,@IDDOCTOR, @APELLIDO, @ESPECIALIDAD, @SALARIO)";
            this.com.Parameters.AddWithValue("@IDDOCTOR", idDoctor);
            this.com.Parameters.AddWithValue("@APELLIDO", apellido);
            this.com.Parameters.AddWithValue("@ESPECIALIDAD", especialidad);
            this.com.Parameters.AddWithValue("@SALARIO", salario);
            this.com.Parameters.AddWithValue("@IDHOSPITAL", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
