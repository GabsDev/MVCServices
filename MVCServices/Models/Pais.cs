using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCServices.Models
{
    public class Pais
    {
        [Display(Name ="País")]
        public String ISO3166 { get; set; }
        public String Nombre { get; set; }

    }
}