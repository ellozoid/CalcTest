using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebCalc.Helpers
{
    public static class DbHelper
    {
        public const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bogdan\Documents\Visual Studio 2015\Projects\CalcTest\WebCalc\App_Data\CalcStorage.mdf"";Integrated Security=True";
        public static IEnumerable<object> GetAllFromTable(string table)
        {
            var sqlquery = $"SELECT * FROM {table}";
            var result = new List<object>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlquery, conn);

                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        var item = new Dictionary<int, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            item.Add(i, reader[i]);
                        }
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public static void InsertTable(string table, IEnumerable<object> item)
        {
            var result = new List<object>();

            var values = item.Select(i => i is string || i is DateTime
                    ? $"'{i}'"
                    : $"{i}"
            );

            var sqlquery = $"INSERT INTO {table} VALUES ({string.Join(", ", values)})";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(sqlquery, conn);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }
        public static void UpdateTable(string table, IEnumerable<object> item)
        {
            var result = new List<object>();

            var values = item.Select(i => i is string || i is DateTime
                    ? $"'{i}'"
                    : $"{i}"
            );

           // var sqlquery = $"UPDATE {table} SET ({string.Join(", ", values)})";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                //var command = new SqlCommand(sqlquery, conn);

                //conn.Open();

                //command.ExecuteNonQuery();

                SqlCommand command = new SqlCommand($"UPDATE {table} SET Result = @Res, ExecutionTime = @ExecTime, ExecutionDate = @ExecDate WHERE OperationName = @OperName AND Arguments = @Args;", conn);
                command.Parameters.AddWithValue("@Res", item.ElementAt(2));
                command.Parameters.AddWithValue("@ExecTime", item.ElementAt(3));
                command.Parameters.AddWithValue("@ExecDate", item.ElementAt(4));
                command.Parameters.AddWithValue("@OperName", item.ElementAt(0));
                command.Parameters.AddWithValue("@Args", item.ElementAt(1));
            }
        }

    }

}
