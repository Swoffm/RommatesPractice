using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");



            Room bathroom = new Room
            {
                Name = "Bathroom",
                MaxOccupancy = 1
            };

            roomRepo.Insert(bathroom);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Room with id {bathroom.Id}");


            Console.WriteLine("-------------------------------");
            Console.WriteLine("-------------------------------");


            Room newBathrrom = new Room
            {
                Name = "Steve's Room",
                MaxOccupancy = 100
            };

            roomRepo.Update(newBathrrom);
            List<Room> newRoom = roomRepo.GetAll();


            foreach (Room room in newRoom)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }


            roomRepo.Delete(bathroom.Id);
            List<Room> w = roomRepo.GetAll();


            foreach (Room room in w)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }

        }
    }
}