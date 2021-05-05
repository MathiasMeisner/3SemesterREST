using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UdpReceiver.Model;

namespace UdpReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UdpReciver receiver = new UdpReciver();

            // starter metoden
            //receiver.ReciverData();

            //Dette er en test for at se om consumer virker.
            //Car car = new Car() { ColorOfCar = "brlue green", IsIn = 1 };

            Car car = new Car();
            
            // starter metoden
            // giver udfyldens krav, så uri, object
            // den bruger generic til at vide hvad er den skal sender til api'en
            Car p = SenderToApi.Post<Car, Car>("https://localhost:44350/api/cars", car).Result;

            // beder programmet om at vis resultet fra metoden i console applikation
            Console.WriteLine("Added: " + p);
        }
    }
}
