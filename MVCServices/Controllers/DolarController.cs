using MVCServices.Models;
using MVCServices.ViewModels;
using System.Web.Mvc;

namespace MVCServices.Controllers
{
    public class DolarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxConsultarASMXBCCR(ViewModelParametro parametro)
        {
            ServiceBCCR oServiceBCCR = new ServiceBCCR();

            var lista = oServiceBCCR.GetDolar(parametro.FechaInicial,
                                              parametro.FechaFinal,
                                              parametro.CompraoVenta);

            return PartialView("_DetalleDolar", lista);
        }


    }
}
