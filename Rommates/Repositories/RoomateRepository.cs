using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;
using Roommates.Repositories;
using Microsoft.VisualBasic;
using System;
using System.Reflection.Metadata;

namespace Rommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        //after a roomate repo is instaniated, the program must pass the connection string to the base repo


        public RoommateRepository(string connectionString) : base(connectionString) { }

        //first lets get all rommates from the repo

        public List<Roommate> GetAll()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "Name" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        //getting value for first name

                        int firstNamePostion = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNamePostion);

                        //getting value of lastname
                        int lastNameLocation = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameLocation);


                        //getting value rent portion

                        int rentPortion = reader.GetOrdinal("RentPortion");
                        int rentPortionValue = reader.GetInt32(rentPortion);
                            
                        //getting value for room id

                        int roomId = reader.GetOrdinal("RoomId");
                        int roomIdValue = reader.GetInt32(rentPortion);

                        //get the value of room and find the room
                        RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
                        Room roommateRoom = roomRepo.GetById(roomIdValue);


                        //getting value for Move IN Date
                        int moveInDate = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInDate);

                        // Now let's create a new roommate object using the data from the database.
                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortionValue,
                            MovedInDate = moveInDateValue,
                            Room = roommateRoom
                        };

                        roommates.Add(roommate);

                    }

                    reader.Close();


                    return roommates;

                }
            }
        }



        public Roommate GetByRoomateId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if(reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName")),
                            Lastname = reader.GetString(reader.GetOrdinal("LastName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                            Room = null
                        };
                    }
                    reader.Close();

                    return roommate;
                }
            }
        }

    }
}
