namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Coche : ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }
        public Coche()
        {
            this.Marca = "Cochecito";
            this.Modelo = "Normal";
            this.Imagen = "bini.png";
            this.Velocidad = 0;
            this.VelocidadMaxima = 100;
        }
        public void Acelerar()
        {
            this.Velocidad += 20;
            if (this.Velocidad > this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }
        public void Frenar()
        {
            this.Velocidad -= 20;
            if (this.Velocidad <= 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
