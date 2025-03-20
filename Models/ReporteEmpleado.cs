using System;

namespace Perfiles_S.A.Models
{
    public class ReporteEmpleado
    {
        public int EmpleadoID { get; set; }
        public string Nombres { get; set; }
        public string DPI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string NIT { get; set; }
        public string NombreDepartamento { get; set; }
        public string Estatus { get; set; }
    }
}