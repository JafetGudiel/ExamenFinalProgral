using System;

namespace p1final2024
{
    public class Articulo  // Cambiado a public
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
    }
}

