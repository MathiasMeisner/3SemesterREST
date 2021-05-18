using _3SemesterREST.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Manager
{
    public class ParkinglotsManager
    {

        public IEnumerable<Parkinglots> GetAll()
        {
            string SelectString = "select * from Parkinglots";

            using (SqlConnection conn = new SqlConnection(Secrets.ConnectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(SelectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Parkinglots> List = new List<Parkinglots>();

                        while (reader.Read())
                        {
                            Parkinglots parkinglots = ReadParkinglots(reader);
                            List.Add(parkinglots);
                        }
                        return List;

                    }
                }
            }
        }

        public int GetCarsByDay(int year, int months, int day)
        {
            string SelectString = $"SELECT COUNT(IsIn) FROM Parkinglots where day='{year}-{months}-{day}'";

            using (SqlConnection conn = new SqlConnection(Secrets.ConnectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(SelectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return Readdata(reader);
                        }
                        return 0;
                    }
                }
            }
        }

        public Parkinglots Add(Parkinglots parkinglots)
        {
            try
            {
                string insertString = "insert into Parkinglots (isin, day) values (@isin, @day);";

                using (SqlConnection conn = new SqlConnection(Secrets.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(insertString, conn))
                    {
                        GuardedAssign(command, "@isin", parkinglots.isin);
                        GuardedAssign(command, "@day", parkinglots.day);

                        int rowsAffected = command.ExecuteNonQuery();

                        return parkinglots;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

            private int Readdata(SqlDataReader reader)
        {
            int numberofcars = reader.GetInt32(0);
            return numberofcars;
        }

        private Parkinglots ReadParkinglots(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            int isin = reader.GetInt32(1);
            DateTime day = GuardedGet<DateTime>(reader, 2);

            Parkinglots parkinglots = new Parkinglots
            {
                id = id,
                isin = isin,
                day = day
            };

            return parkinglots;
        }

        private static T GuardedGet<T>(SqlDataReader reader, int column)
        {
            if (reader.IsDBNull(column))
            {
                return default(T);
            }
            return reader.GetFieldValue<T>(column);
        }

        private static void GuardedAssign<T>(SqlCommand command, string parmeterName, T value)
        {
            if (value == null)
            {
                command.Parameters.AddWithValue(parmeterName, DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(parmeterName, value);
            }
        }
    }
}
