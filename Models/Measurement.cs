using Npgsql;

namespace WeatherApp.Models
{
    public class Measurement
    {
        public int Id {get; set;}
        public string Name {get; set;} 
        public float Temperature {get; set;} 
        public float Humidity {get; set;}
        public int Pressure {get; set;}
        public string Mac {get; set;}
        public int MovementCounter {get; set;}
        public DateTime Date {get; set;}

        public Measurement() {}

        public void SearchFromDatabase(string sqlquery)
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=example;Database=postgres";
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using (var cmd = new NpgsqlCommand(sqlquery, connection))
            {
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Id = (int)reader["id"];
                    Name = (string)reader["name"];
                    Temperature = (float)reader["temperature"];
                    Humidity = (float)reader["humidity"];
                    Pressure = (int)reader["pressure"];
                    Mac = (string)reader["mac"];
                    MovementCounter = (int)reader["movementcounter"];
                    Date = (DateTime)reader["date"];
                }
            }
            connection.Close();
        }
    }
}