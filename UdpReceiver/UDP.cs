using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UdpReceiver.Model;

namespace UdpReceiver
{
    public class UDP
    {
        UdpClient udpServer = new UdpClient(9999);
        /// <summary>
        /// Denne metode modtager oplysninger fra vores raspberry pi
        /// </summary>
        public void ReciverData()
        {
            // bestemmer hvilken port programmet skal modtager oplysninger 
            

            // laver ip adresse om til en string
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            // her bliver afgjort hvilken ip adresse den modtager fra
            // og bestemt hvilken port den modtager i.
            IPEndPoint RemoteIPEndpoint = new IPEndPoint(ip, 9999);

            try
            {
                Car car = new Car();
                Parkinglots parkinglots = new Parkinglots();

                // laver de modtaget oplysninger om byte array
                Byte[] recivedBytes = udpServer.Receive(ref RemoteIPEndpoint);
                Console.WriteLine("modtaget");
                // laver oplysninng om fra byte til string
                string recivedData = Encoding.ASCII.GetString(recivedBytes);

                // splidt oplysning op i hver deres felt af et string array
                string[] data = recivedData.Split(" ");

                // ligger oplysninger hen modellens properties 
                car.Color = data[0];

                car.LicensePlate = data[3];

                // laver det om til tal fra string inden bliver lagt over i properties
                parkinglots.isin = Int32.Parse(data[1]);

                parkinglots.day = DateTime.Parse(data[2]);

                Console.WriteLine(car.Color + " " + parkinglots.isin + " " + parkinglots.day, " " + car.LicensePlate);

                Car c = Consumer.PostToCar<Car, Car>("https://localhost:44350/api/cars", car).Result;
                Parkinglots p = Consumer.PostToparkinglot<Parkinglots, Parkinglots>("https://localhost:44350/api/parkinglots", parkinglots).Result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
