using System.Reflection;
using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;

namespace UserSideWEB.DataLayer
{
    public class ORM
    {
        private SqliteConnection connection;

        /// <summary>
        /// Creates a ORM object that is used foracessing the database. Constructor accepts SQLite connection string.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        public ORM(String connectionString)
        {
            this.connection = new SqliteConnection(connectionString);
            Init().Wait();
        }

        public async Task Init()
        {
            using(var connection  = this.connection)
            {
                await connection.OpenAsync();
                string commandString = "SELECT COUNT(*) FROM sqlite_master WHERE type=@type AND name=@name";
                int count = 0;
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    command.Parameters.AddWithValue("type", "table");
                    command.Parameters.AddWithValue("name", "HighSchool");
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            count = reader.GetInt32(0);
                        }

                    }
                }
                //Jestlize tabulka neexistuje, tak dojde k vytvoreni databaze a naplneni hodnotami 
                if (count != 1)
                {
                    //Vytvoreni tabulek, cte radek po radku dokud nenarazi na ;, pak provede query, vymaze commandString a cte dale 
                    string path = @"table_creation.sql";
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string? line = await sr.ReadLineAsync();
                        while(line != null && line != "")
                        {
                            if(line.Contains(";"))
                            {
                                commandString = line;
                                using(var command = connection.CreateCommand())
                                {
                                    command.CommandText = commandString;
                                    await command.ExecuteNonQueryAsync();
                                }
                                line = null;
                            }
                            line += await sr.ReadLineAsync();
                        }
                    }
                    //Naplneni tabulek hodnotami, cte radek po radku a pokud narazi na insert, tak jej provede
                    path = @"data_insert.sql";
                    using(StreamReader sr = new StreamReader(path))
                    {
                        string? line = await sr.ReadLineAsync();
                        while(line != null)
                        {
                            if(line.Contains("insert"))
                            {
                                commandString = line;
                                using(var command = connection.CreateCommand())
                                {
                                    command.CommandText = commandString;
                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            line = await sr.ReadLineAsync();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get's a user from the database based on the id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Get<T,IDType>(IDType id)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            object output = Activator.CreateInstance(type);
            using (var connection = this.connection)
            {
                await connection.OpenAsync();

                String commandString = "select ";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    commandString += propertyInfo.Name;

                    // trailing commas, yay
                    PropertyInfo last = propertyInfos.Last();
                    if (propertyInfo == last)
                    {
                        commandString += " ";
                    }
                    else
                    {
                        commandString += ", ";
                    }
                }

                commandString += "from " + type.Name + " where " + propertyInfos[0].Name + " = @Id";
                Console.WriteLine(commandString);


                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            foreach (var propertyInfo in propertyInfos)
                            {
                                if (propertyInfo.PropertyType == typeof(DateTime))
                                {
                                    propertyInfo.SetValue(output, reader.GetDateTime(reader.GetOrdinal(propertyInfo.Name)), null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(output, reader[propertyInfo.Name], null);
                                }
                                //propertyInfo.SetValue(output, reader[propertyInfo.Name], null);
                            }
                        }
                    }
                }
            }
            return (T)output;
        }

        /// <summary>
        /// Updates the value in database if it exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public async Task Update<T>(T value)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            using (var connection = this.connection)
            {
                await connection.OpenAsync();
                String commandString = "update " + type.Name + " set ";
                //i starts at 1 to skip id
                for(int i = 1;i<propertyInfos.Length;i++)
                {
                    commandString += propertyInfos[i].Name + " = @" + propertyInfos[i].Name;
                    // trailing commas, yay
                    PropertyInfo last = propertyInfos.Last();
                    if (propertyInfos[i] == last)
                    {
                        commandString += " ";
                    }
                    else
                    {
                        commandString += ", ";
                    }
                }
                commandString += "where " + propertyInfos[0].Name + " = @OldId";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    //i starts at 1 to skip id
                    for(int i = 1;i<propertyInfos.Length;i++)
                    {
                        command.Parameters.AddWithValue(propertyInfos[i].Name, propertyInfos[i].GetValue(value));
                    }
                    command.Parameters.AddWithValue("OldId", propertyInfos[0].GetValue(value));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Inserts row into table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Row to be added</param>
        /// <param name="autoID">If table uses auto_increment or no</param>
        /// <returns></returns>
        public async Task Insert<T>(T value, bool autoID = true)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            int i = 0;
            if(autoID)
            {
                //Skip first column
                i = 1;
            }
            using (var connection = this.connection)
            {
                await connection.OpenAsync();
                String commandString = "insert into " + type.Name + " (";
                for(;i<propertyInfos.Length;i++)
                {
                    commandString += propertyInfos[i].Name;
                    // trailing commas, yay
                    PropertyInfo last = propertyInfos.Last();
                    if (propertyInfos[i] == last)
                    {
                        commandString += ") ";
                    }
                    else
                    {
                        commandString += ", ";
                    }
                }
                i = 0;
                if (autoID)
                {
                    //Skip first column
                    i = 1;
                }
                commandString += "values (";
                for (; i < propertyInfos.Length; i++)
                {
                    commandString += "@" + propertyInfos[i].Name;
                    // trailing commas, yay
                    PropertyInfo last = propertyInfos.Last();
                    if (propertyInfos[i] == last)
                    {
                        commandString += ") ";
                    }
                    else
                    {
                        commandString += ", ";
                    }
                }
                i = 0;
                if (autoID)
                {
                    //Skip first column
                    i = 1;
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    for(;i<propertyInfos.Length;i++)
                    {
                        command.Parameters.AddWithValue(propertyInfos[i].Name, propertyInfos[i].GetValue(value));
                    }
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<int> GetCount<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            using (var connection = this.connection)
            {
                await connection.OpenAsync();
                String commandString = "select count(*) from " + type.Name;
                Console.WriteLine(commandString);
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return 0;
        }

        public async Task<List<T>> GetAll<T>()
        {
            //Získáme typ třídy, která reprezentuje tabulku
            Type type = typeof(T);
            //Získáme properties třídy, které reprezentují jednotlivé sloupce
            PropertyInfo[] propertyInfos = type.GetProperties();
            List<T> list = new List<T>();
            //Začátek komunikace
            using (connection = this.connection)
            {
                await connection.OpenAsync();
                String commandString = "select ";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    commandString += propertyInfo.Name;
                    // trailing commas, yay
                    PropertyInfo last = propertyInfos.Last();
                    if (propertyInfo == last)
                    {
                        commandString += " ";
                    }
                    else
                    {
                        commandString += ", ";
                    }
                }
                commandString += "from " + type.Name;
                using (var command = connection.CreateCommand())
                {
                    command.CommandText += commandString;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            object output = Activator.CreateInstance(type);
                            foreach (var propertyInfo in propertyInfos)
                            {
                                if (propertyInfo.PropertyType == typeof(DateTime))
                                {
                                    propertyInfo.SetValue(output, reader.GetDateTime(reader.GetOrdinal(propertyInfo.Name)), null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(output, reader[propertyInfo.Name], null);
                                }
                                //propertyInfo.SetValue(output, reader[propertyInfo.Name], null);
                            }
                            list.Add((T)output);
                        }
                    }
                }
            }
            return list;
        }


        public async Task Delete<T>(long id, int Idposition = 0)
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            using (var connection = this.connection)
            {
                await connection.OpenAsync();
                String commandString = "delete from " + type.Name + " where "+ propertyInfos[Idposition].Name +" = @Id";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandString;
                    command.Parameters.AddWithValue("Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
