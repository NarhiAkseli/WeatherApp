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

        public void SearchFromDatabase(NpgsqlDataReader reader)
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
}