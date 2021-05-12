using _3SemesterREST.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Manager
{
    public class ProductOwnerManager
    {
        public int GetCarsByDay()
        {
            string SelectString = $"SELECT COUNT(IsIn) FROM Cars where TodayDate='2021-05-12'";

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
        private int Readdata(SqlDataReader reader)
        {
            int numberofcars = reader.GetInt32(0);
            return numberofcars;
        }
    }
}
