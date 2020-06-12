using System;

namespace Quotation.API.Model
{
    public class Quotation
    {
        public string Moneda { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }
        public string Fecha { get; set; }
    }
}