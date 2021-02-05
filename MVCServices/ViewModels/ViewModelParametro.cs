using System;
using System.ComponentModel.DataAnnotations;

namespace MVCServices.ViewModels
{
    public class ViewModelParametro
    {
        [Display(Name = "Fecha Inicial" )]
        [DataType(DataType.Date)]
        public DateTime FechaInicial { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        public DateTime FechaFinal { get; set; }
        [Display(Name = "Compra / Venta")]
        public String CompraoVenta { get; set; }

    }
}