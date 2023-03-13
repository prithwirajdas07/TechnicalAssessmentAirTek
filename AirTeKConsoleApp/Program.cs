using System;
using System.Collections.Generic;
using System.IO;
using AirTeKConsoleApp.Helper;
using AirTeKConsoleApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AirTeKConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load the flight schedule data
            var flights = LoadFlights();

            // Display the flight schedule
            Console.WriteLine("Flight Schedule:");
            DisplayFlights(flights);

            // Load the orders data
            var orders = LoadOrders();

            // Schedule the orders on the flights
            var scheduledFlights = ScheduleOrders(flights, orders);

            // Display the scheduled flights with their corresponding orders
            Console.WriteLine("Scheduled Flights:");
            DisplayScheduledFlights(scheduledFlights);

            Console.ReadKey();
        }

        // Possible Update: Move this code to a separate Service class
        // Possible Update: make the departure and arrival cities configurable by passing them as arguments to the function, so that we can use the same function to schedule flights from other cities
        static List<Flight> LoadFlights()
        {
            // Define the flight schedule data
            var flightSchedule = new List<Flight>
            {
                new Flight { Number = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 },
                new Flight { Number = 2, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 1 },
                new Flight { Number = 3, DepartureCity = "YUL", ArrivalCity = "YVR", Day = 1 },
                new Flight { Number = 4, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 2 },
                new Flight { Number = 5, DepartureCity = "YUL", ArrivalCity = "YYC", Day = 2 },
                new Flight { Number = 6, DepartureCity = "YUL", ArrivalCity = "YVR", Day = 2 },
            };

            return flightSchedule;
        }


        // Possible Update: Move this code to a separate Service class
        static void DisplayFlights(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight: {flight.Number}, departure: {flight.DepartureCity}, " +
                    $"arrival: {flight.ArrivalCity}, day: {flight.Day}");
            }
            Console.WriteLine();
        }

        // Possible Update: Move this code to a separate Service class
        static List<Order> LoadOrders()
        {
            // Read the orders data from the JSON file
            var json = File.ReadAllText("orders.json");

            var orders = JsonConvert.DeserializeObject<List<Order>>(json, new OrderListConverter());
            
            //var ordersDict = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            //List<Order> orders = new List<Order>(ordersDict.Values);
            return orders;
        }

        // Possible Update: Move this code to a separate Service class
        static List<ScheduledFlight> ScheduleOrders(List<Flight> flights, List<Order> orders)
        {
            // Group the orders by destination city
            var groupedOrders = orders.GroupBy(o => o.Destination);

            // Initialize the scheduled flights
            var scheduledFlights = new List<ScheduledFlight>();

            // Loop through the flights
            foreach (var flight in flights)
            {
                // Find the orders that match the flight's destination
                var flightOrders = new List<Order>();
                foreach (var group in groupedOrders)
                {
                    if (group.Key == flight.ArrivalCity)
                    {
                        flightOrders = group.ToList();
                        break;
                    }
                }

                // Schedule the orders on the flight
                var scheduledFlight = new ScheduledFlight { Flight = flight };
                foreach (var order in flightOrders)
                {
                    if (scheduledFlight.Orders.Count == flight.Capacity)
                    {
                        break;
                    }
                    scheduledFlight.Orders.Add(order);
                }

                // Remove the scheduled orders from the orders list
                foreach (var order in scheduledFlight.Orders)
                {
                    var orderToRemove = orders.FirstOrDefault(o => o.Id == order.Id);
                    if (orderToRemove != null)
                    {
                        orders.Remove(orderToRemove);
                    }
                }

                // Add the scheduled flight to the list
                scheduledFlights.Add(scheduledFlight);
            }

            // Add any unscheduled orders to the list
            foreach (var order in orders)
            {
                var scheduledFlight = new ScheduledFlight { Flight = null, Orders = new List<Order> { order } };
                scheduledFlights.Add(scheduledFlight);
            }
            return scheduledFlights;
        }

        // Possible Update: Move this code to a separate Service class
        static void DisplayScheduledFlights(List<ScheduledFlight> scheduledFlights)
        {
            var unsheduledOrders = new List<Order>();
            
            // Loop through the scheduled flights
            foreach (var scheduledFlight in scheduledFlights)
            {

                if (scheduledFlight.Flight == null)
                {
                    foreach (var order in scheduledFlight.Orders)
                    {
                        unsheduledOrders.Add(order);
                    }
                }
                else
                {
                    // Display the flight number and destination city
                    Console.WriteLine($"Flight {scheduledFlight.Flight?.Number ?? 0}: {scheduledFlight.Flight?.DepartureCity} -> {scheduledFlight.Flight?.ArrivalCity}");

                    // Display the scheduled orders
                    foreach (var order in scheduledFlight.Orders)
                    {
                        //Console.WriteLine($"- Order {order.Id} to {order.Destination}");
                        Console.WriteLine($"order: {order.Id}, flightNumber: {scheduledFlight.Flight?.Number ?? 0}, " + $"departure: {scheduledFlight.Flight?.DepartureCity}, arrival: {scheduledFlight.Flight?.ArrivalCity}, day: {scheduledFlight.Flight?.Day}");
                    }
                    Console.WriteLine();
                }
            }
            
            // Display The unscheduled orders
            
            Console.WriteLine("Unscheduled Flights:");
            foreach (var order in unsheduledOrders)
            {
                Console.WriteLine($"- Order {order.Id} to {order.Destination}");
            }
            Console.WriteLine();
        }
    }
    
}