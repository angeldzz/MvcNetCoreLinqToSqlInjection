namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Deportivo: ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }
        public Deportivo()
        {
            this.Marca = "Batmovil";
            this.Modelo = "Guay";
            this.Imagen = "bellingham.png";
            this.VelocidadMaxima = 320;
            this.Velocidad = 0;
        }
        public void Acelerar()
        {
            this.Velocidad += 50;
            if (this.Velocidad > this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }

        public void Frenar()
        {
            this.Velocidad -= 50;
            if (this.Velocidad <= 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
