using System.Data.Entity;



    public class MeasurementDbContext : DbContext
    {
        public DbSet<Measurement> Measurements {get; set;}
    }
}
