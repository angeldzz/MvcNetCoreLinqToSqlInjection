namespace MvcNetCoreLinqToSqlInjection.Models
{
    public interface ICoche
    {
        //La interface no tiene ambito de metodo
        //ni lleva public, private, protected, solo la delcaracion
        string Marca { get; set; }
        string Modelo { get; set; }
        string Imagen { get; set; }
        int Velocidad { get; set; }
        int VelocidadMaxima { get; set; }
        void Acelerar();
        void Frenar();
    }
}
