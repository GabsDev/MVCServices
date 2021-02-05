using MVCServices.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MVCServices.Models
{
    public class ServiceCovid
    {
        // Funcionamiento de Tuple https://www.tutorialsteacher.com/csharp/csharp-tuple
        public Tuple<string, string, string, List<ViewModelDetalleCovid>> GetCovidByCountry(Pais pPais)
        {

            List<ViewModelDetalleCovid> lista = null;
            string etiquetas = "";
            string valores = "";
            string colores = "";
            string responseBody = "";
             
            var url = $"https://api.covid19api.com/country/{pPais.Nombre}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json"; 

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                        return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        responseBody = objReader.ReadToEnd();
                    }
                }
            }

            // Deserealización 
            lista = JsonConvert.DeserializeObject<List<ViewModelDetalleCovid>>(responseBody);

            // Extraer solo los que posee muertes y los primero 10 para pruebas
            // lista = lista.FindAll(p => p.Deaths > 0).Take(10).ToList();
            lista = lista.FindAll(p => p.Deaths > 0).ToList();

            foreach (var item in lista)
            {
                // Concatenar para que vaya así xxx,ttt,hhh...
                etiquetas +=  item.Date.ToString("dd/MM/yyyy") + ",";
                valores +=   item.Deaths.ToString("##") + ",";
            }
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1); // ultima coma
            valores = valores.Substring(0, valores.Length - 1);

            // Genera un color diferente por cada elemento
            var colors = GenerateColors(lista.Count());
            // toma la lista y le agrega separa por comas (,)
            colores = string.Join(",", colors.ToList());

            // Crea la tupla para responderla
            var retornoTupla = Tuple.Create(colores, etiquetas, valores, lista);

            return retornoTupla;

        }

        /// <summary>
        /// Hay que generar un color por cada item para que se vea bonito el gráfico.
        /// </summary>
        /// <param name="pCantidad"></param>
        /// <returns></returns>
        private List<string> GenerateColors(int pCantidad)
        {             
            var colors = new List<string>();
            var random = new Random(); // Make sure this is out of the loop!
            for (int i = 0; i < pCantidad; i++)
            {
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }
            return colors;
        }


    }
}