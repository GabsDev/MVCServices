﻿using MVCServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVCServices.Models
{
    public class ServiceBCCR
    {
        private readonly string TOKEN = "88RE0A2MEE";
        private readonly string NOMBRE = "Luis Gabriel Avila Medrano";
        private readonly string CORREO = "lavila@est.utn.ac.cr";

        public IEnumerable<ViewModelDetalleDolar> GetDolar(DateTime pFechaInicial, DateTime pFechaFinal, String pCompraoVenta)
        {

            List<ViewModelDetalleDolar> lista = new List<ViewModelDetalleDolar>();
            DataSet dataset = null;
            string fecha_inicial, fecha_final;
            string tipoCompraoVenta;

            // Se convierten las fechas a string en el formato solicitado
            fecha_inicial = pFechaInicial.ToString("dd/MM/yyyy");
            fecha_final = pFechaFinal.ToString("dd/MM/yyyy");

            // se compara si es compra o venta 318 o 317
            tipoCompraoVenta = pCompraoVenta.Equals("c", StringComparison.InvariantCulture) ? "317" : "318";

            // Protocolo de comunicaciones
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            // Se instancia al Servicio Web
            BCCR.wsindicadoreseconomicosSoapClient client =
                new BCCR.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");

            // Se invoca.
            dataset = client.ObtenerIndicadoresEconomicos(tipoCompraoVenta, fecha_inicial,
                          fecha_final, NOMBRE, "N", CORREO, TOKEN);

            DataTable table = dataset.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                // Validar el error. No es la forma correcta pero bueno.
                if (row[0].ToString().Contains("error"))
                {
                    throw new Exception(row[0].ToString());
                }

                ViewModelDetalleDolar dolar = new ViewModelDetalleDolar();
                dolar.Codigo = row[0].ToString();
                dolar.Fecha = row[1].ToString();
                dolar.Monto = Convert.ToDouble(row[2].ToString());
                lista.Add(dolar);
            }

            return lista;
        }

    }
}   