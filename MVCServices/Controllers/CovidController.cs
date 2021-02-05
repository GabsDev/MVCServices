using MVCServices.Models;
using MVCServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace MVCServices.Controllers
{
    public class CovidController : Controller
    {
        private readonly int CANTIDADREGISTROS = 5;
        public ActionResult Index()
        {
            ViewBag.Lista = GetPais();
            return View();
        }

        public ActionResult AjaxConsultarCovid(Pais pais)
        {

            ServiceCovid oService = new ServiceCovid();
            List<ViewModelDetalleCovid> listaDetalleCovid = null;
            var tupla = oService.GetCovidByCountry(pais);

            ViewBag.Colores = tupla.Item1;
            ViewBag.Etiquetas = tupla.Item2;
            ViewBag.Valores = tupla.Item3;
            // parametro de la tupla que es la lista
            listaDetalleCovid = tupla.Item4;
            // Forzar error             //string x = "123".Substring(0, 4444);
            // Almacenar los datos
            TempData["Data"] = listaDetalleCovid;
            TempData["CurrentPage"] = 1;
            // La cantidad de registros por mostrar
            TempData["PageCount"] = Convert.ToInt32(Math.Ceiling(listaDetalleCovid.Count() / (double)CANTIDADREGISTROS));
            TempData.Keep();
            // Se filtra para solo enviar la cantidad que se va a mostrar.           
            listaDetalleCovid = listaDetalleCovid.OrderBy(b => b.Date).Skip(0).Take(CANTIDADREGISTROS).ToList();

            // Se invoca la vista parcial 
            return PartialView("_DetalleCovid", listaDetalleCovid);


        }

        // Paginación con los datos que están en TempData["Data"], recibe el no  de página siguiente
        public ActionResult Paginar(int page)
        {

            List<ViewModelDetalleCovid> listaDetalleCovid = (List<ViewModelDetalleCovid>)TempData["Data"];
            TempData["PerPage"] = CANTIDADREGISTROS;
            TempData["CurrentPage"] = page;
            // TempData["PageCount"] = listaDetalleCovid.Count;
            int start = (page - 1) * CANTIDADREGISTROS;
            listaDetalleCovid = listaDetalleCovid.OrderBy(b => b.Date).Skip(start).Take(CANTIDADREGISTROS).ToList();
            TempData.Keep();

            return PartialView("_DetalleCovid", listaDetalleCovid);
        }


        private List<Pais> GetPais()
        {
            string[] paises = { "CRI,Costa Rica","PAN,Panama","MEX,Mexico","COL,Colombia",
                        "URU,Uruguay","Bol,Bolivia","BEL,Belize"  };

            List<Pais> lista = new List<Pais>();

            foreach (string item in paises)
            {
                Pais pais = new Pais()
                {
                    ISO3166 = item.Split(',')[0],
                    Nombre = item.Split(',')[1]
                };
                lista.Add(pais);
            }

            return lista;
        }

    }
}
