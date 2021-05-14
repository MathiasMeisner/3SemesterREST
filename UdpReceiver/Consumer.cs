using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdpReceiver.Model;

namespace UdpReceiver
{
    public class Consumer
    {
        /// <summary>
        /// metode laver en http post reqeust til api'en. 
        /// </summary>
        public static async Task<TOut> PostToCar<TIn, TOut>(string uri, TIn item)
        {

            using HttpClient client = new HttpClient();

            // serialize car object til json, som bruges som en string 
            string CarObject = JsonConvert.SerializeObject(item);

            // gøre oplysningerne klar til at blive sendt til api
            StringContent requestContent = new StringContent(CarObject, Encoding.UTF8, "application/json");

            // sender en post request til vores api med oplysningerne fra raspberry pi
            HttpResponseMessage response = await client.PostAsync(uri, requestContent);

            // venter på svar fra api
            string responseContent = await response.Content.ReadAsStringAsync();

            // hvis det er gået godt, går vi inden i if sætning krop (gælder alle http statuskoder mellem 200 til 299)
            if (response.IsSuccessStatusCode)
            {
                // vi deserialize json, som vi har modtaget fra api
                TOut data = JsonConvert.DeserializeObject<TOut>(responseContent);
                return data;
            }
            
            // vi kaster kun exception, hvis det er gået galt
            //exception giver besked tilbage med http stauskode og response content fra api
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }
        public static async Task<TOut> PostToparkinglot<TIn, TOut>(string uri, TIn item)
        {

            using HttpClient client = new HttpClient();

            // serialize car object til json, som bruges som en string 
            string CarObject = JsonConvert.SerializeObject(item);

            // gøre oplysningerne klar til at blive sendt til api
            StringContent requestContent = new StringContent(CarObject, Encoding.UTF8, "application/json");

            // sender en post request til vores api med oplysningerne fra raspberry pi
            HttpResponseMessage response = await client.PostAsync(uri, requestContent);

            // venter på svar fra api
            string responseContent = await response.Content.ReadAsStringAsync();

            // hvis det er gået godt, går vi inden i if sætning krop (gælder alle http statuskoder mellem 200 til 299)
            if (response.IsSuccessStatusCode)
            {
                // vi deserialize json, som vi har modtaget fra api
                TOut data = JsonConvert.DeserializeObject<TOut>(responseContent);
                return data;
            }

            // vi kaster kun exception, hvis det er gået galt
            //exception giver besked tilbage med http stauskode og response content fra api
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }

    }
}
