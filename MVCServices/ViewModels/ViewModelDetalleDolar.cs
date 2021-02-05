using System.ComponentModel.DataAnnotations;

namespace MVCServices.ViewModels
{
    public class ViewModelDetalleDolar
    {
        public string Codigo { get; set; }        
        public string Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}" )]   
        public double Monto { get; set; }

    }
}