using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public static class Database
    {
        public static async Task<string> LookUp(ILogger<Worker> logger)
        {
            string name = default;

            using (var db = new MySqlConnection("server=localhost;user id=root;password=gv4319!!;port=3306;database=dotnet;"))
            {
                try
                {
                    var command = db.CreateCommand();

                    var query = @"SELECT
                                    UserName
                                   FROM users 
                                    WHERE Id = 1";

                    command.CommandText = query;

                   await db.OpenAsync();

                    var reader = await command.ExecuteReaderAsync();

                    while(await reader.ReadAsync())
                    {
                        name = (string)reader["UserName"];
                    }
                    
                }
                catch(Exception ex)
                {
                    logger.LogError($"Something gone wrong - error {ex.ToString()}");
                }
            }

            return name;
        }
    }
}
