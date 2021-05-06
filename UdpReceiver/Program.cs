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
            //Car car = new Car();

            UDP UDP = new UDP();
            Console.WriteLine("welcome");
            // starter metoden
            while (true)
            {
                UDP.ReciverData();
            }
            

            //Dette er en test for at se om consumer virker.
            //Car car = new Car() { ColorOfCar = "blue green", IsIn = 1 };

            

            // starter metoden
            // giver udfyldens krav, så uri, object
            // den bruger generic til at vide hvad er den skal sender til api'en
            //Car p = Consumer.Post<Car, Car>("https://localhost:44350/api/cars", car).Result;

            // beder programmet om at vis resultet fra metoden i console applikation
            //Console.WriteLine("Added: " + p);
            
        }
    }
}
