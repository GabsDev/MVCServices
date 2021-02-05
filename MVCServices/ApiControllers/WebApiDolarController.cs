using MVCServices.Models;
using System;
using System.Web.Http;

namespace MVCServices.ApiControllers
{
    public class WebApiDolarController : ApiController
    {
        public IHttpActionResult GetDolarLongWay(string fechaInicial,
                                                string fechaFinal,
                                                string compraOVenta)
        {
            try
            {
                ServiceBCCR oServiceBCCR = new ServiceBCCR();

                // Debe validar que las fechas sean validas DAteTime.TryParse
                // Debe validar que compraoVenta sea “c” o “v” (compra o venta)

                var lista = oServiceBCCR.GetDolar(DateTime.Parse(fechaInicial),
                                                  DateTime.Parse(fechaFinal), compraOVenta);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Redireccion a la captura del Error
                return Ok("Error");
            }
        }

        [Route("dolar/{fechaInicial}/{fechaFinal}/{compraOVenta}")]
        [HttpGet]
        public IHttpActionResult GetDolar(string fechaInicial,
            string fechaFinal,
            string compraOVenta)
        {
            try
            {
                ServiceBCCR oServiceBCCR = new ServiceBCCR();

                // Debe validar que las fechas sean validas DAteTime.TryParse
                // Debe validar que compraoVenta sea c o v

                var lista = oServiceBCCR.GetDolar(DateTime.Parse(fechaInicial), DateTime.Parse(fechaFinal),
                                          compraOVenta);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Redireccion a la captura del Error
                return Ok("Error");
            }
        }

        [Route("dolar/hoy/compra")]
        [HttpGet]
        public IHttpActionResult GetCompra()
        {

            try
            {
                ServiceBCCR oServiceBCCR = new ServiceBCCR();
                // Debe validar que las fechas sean validas DAteTime.TryParse
                // Debe validar que compraoVenta sea c o v
                var lista = oServiceBCCR.GetDolar(DateTime.Now, DateTime.Now, "c");
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Redireccion a la captura del Error
                return Ok("Error");
            }
        }

        [Route("dolar/hoy/venta")]
        [HttpGet]
        public IHttpActionResult GetVenta()
        {
            try
            {
                ServiceBCCR oServiceBCCR = new ServiceBCCR();
                // Debe validar que las fechas sean validas DAteTime.TryParse
                // Debe validar que compraoVenta sea c o v
                var lista = oServiceBCCR.GetDolar(DateTime.Now, DateTime.Now, "v");
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Redireccion a la captura del Error
                return Ok("Error");
            }
        }

    }
}
