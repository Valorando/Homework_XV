/*Написать программу которая будет принимать у пользователя название БД и создать ее. 
После чего запрашивать у пользователя Название таблицы и создать ее с полями которые нужны пользователю. 
После чего дать возможность ему наполнить ее) */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_24_08
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string server_name;
                string database_name;
                string table_name;
                string fields;
                string values;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Введите имя сервера к которому требуется подключится: ");
                server_name = Console.ReadLine();
                Console.WriteLine();

                string connection_to_server = $@"Data Source = {server_name}; Initial Catalog = master; Trusted_Connection=True; TrustServerCertificate= True";


                using (SqlConnection connection = new SqlConnection(connection_to_server))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();

                    Console.Write("Введите имя базы данных которую требуется создать: ");
                    database_name = Console.ReadLine();
                    Console.WriteLine();

                    command.CommandText = $"CREATE DATABASE {database_name}";

                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();



                    string connection_to_database = $@"Data Source = {server_name}; Initial Catalog = {database_name}; Trusted_Connection=True; TrustServerCertificate= True";

                    using (SqlConnection connection1 = new SqlConnection(connection_to_database))
                    {
                        await connection1.OpenAsync();
                        SqlCommand command1 = new SqlCommand();

                        Console.Write("Введите название таблицы которую требуется создать: ");
                        table_name = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine($"Через запятую введите поля для таблицы {table_name} по форме *имя переменной* *тип данных* *условие, если нужно*");
                        Console.Write("(Пример: Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(100): ");
                        fields = Console.ReadLine();
                        Console.WriteLine();

                        command1.CommandText = $"CREATE TABLE {table_name}({fields})";

                        command1.Connection = connection1;
                        await command1.ExecuteNonQueryAsync();


                    }



                    string connection_to_table = $@"Data Source = {server_name}; Initial Catalog = {database_name}; Trusted_Connection=True; TrustServerCertificate= True";

                    using (SqlConnection connection2 = new SqlConnection(connection_to_table))
                    {

                        Console.WriteLine($"Через запятую введите значения для полей таблицы {table_name} по форме (*значение*, *значение*), (*значение*, *значение*).");
                        Console.Write("Пример: ('Ivan', 43), ('Petr' 18): ");
                        values = Console.ReadLine();
                        Console.WriteLine();

                        await connection2.OpenAsync();
                        SqlCommand command2 = new SqlCommand();

                        command2.CommandText = $"INSERT INTO {table_name} VALUES ({values})";


                        command2.Connection = connection2;
                        await command2.ExecuteNonQueryAsync();

                        Console.WriteLine("Процедура завершена, нажмите любую клавишу для выхода.");
                        Console.ReadKey();
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для выхода.");
                Console.ReadKey();
            }
        }
    }
}
