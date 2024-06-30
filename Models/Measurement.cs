namespace Measurement.Models
{
    public class Measurement
    {
        public int Id {get; set;}
        public string Name {get; set;} 
        public float Temperature {get; set;} 
        public float Humidity {get; set;}
        public string Mac {get; set;}
        public int MovementCounter {get; set;}
        public DateTime Date {get; set;}
    }
}