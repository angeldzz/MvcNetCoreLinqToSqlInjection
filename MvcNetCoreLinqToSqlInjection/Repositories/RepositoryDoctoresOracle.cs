using MvcNetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    #region STORED PROCEDURE
    /*
create or replace procedure SP_DELETE_DOCTOR
(p_iddoctor DOCTOR.DOCTOR_NO%type)
AS
BEGIN
    DELETE FROM DOCTOR
    WHERE DOCTOR_NO = p_iddoctor;
    COMMIT;
end;
create or replace procedure SP_UPDATE_DOCTOR
(p_idhospital DOCTOR.HOSPITAL_COD%type, p_iddoctor DOCTOR.DOCTOR_NO%type, p_apellido DOCTOR.APELLIDO%type, p_especialidad DOCTOR.ESPECIALIDAD%type, p_salario DOCTOR.SALARIO%type)
AS
BEGIN
    UPDATE DOCTOR SET HOSPITAL_COD=p_idhospital, DOCTOR_NO=p_iddoctor,APELLIDO=p_apellido,ESPECIALIDAD=p_especialidad,SALARIO=p_salario 
    WHERE DOCTOR_NO = p_iddoctor;
    COMMIT;
end;
*/
    #endregion
    public class RepositoryDoctoresOracle: IRepositoryDoctores
    {

        private DataTable tablaDoctor;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=localhost:1521/FREEPDB1;Persist Security Info=True;User Id=SYSTEM; Password=oracle;";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            this.tablaDoctor = new DataTable();
            string sql = "SELECT * FROM DOCTOR";
            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
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
        public async Task CreateDoctorAsync(int idDoctor, string apellido, string especialidad,
                                        int salario, int idHospital)
        {
            string sql = "INSERT INTO DOCTOR VALUES (:IDHOSPITAL,:IDDOCTOR, :APELLIDO, :ESPECIALIDAD, :SALARIO)";
            //AQUI VAN LOS PARAMETROS Los cuales cambian de SQL a ORACLE;
            OracleParameter pamIdHospital = new OracleParameter(":IDHOSPITAL", idHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":IDDOCTOR", idDoctor);
            OracleParameter pamApellido = new OracleParameter(":APELLIDO", apellido);
            OracleParameter pamEspecialidad = new OracleParameter(":ESPECIALIDAD", especialidad);
            OracleParameter pamSalario = new OracleParameter(":SALARIO", salario);
            this.com.Parameters.Add(pamIdHospital);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamSalario);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            await this.cn.CloseAsync();
        }

        public async Task DeleteDoctorAsync(int iddoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            OracleParameter pamIdDoctor = new OracleParameter(":P_IDDOCTOR", iddoctor);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            await this.cn.CloseAsync();
        }
        public async Task UpdateDoctorAsync(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "SP_UPDATE_DOCTOR";
            this.com.Parameters.Add(":p_idhospital", idHospital);
            this.com.Parameters.Add(":p_iddoctor", idDoctor);
            this.com.Parameters.Add(":p_apellido", apellido);
            this.com.Parameters.Add(":p_especialidad", especialidad);
            this.com.Parameters.Add(":p_salario", salario);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            this.com.Parameters.Clear();
            await this.cn.CloseAsync();
        }
        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD").ToUpper().StartsWith(especialidad.ToUpper())
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }
    }
}
