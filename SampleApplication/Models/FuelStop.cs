using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;




namespace SampleApplication.Models


{
    public class FuelStop

    {
        [BsonId]
       [BsonRepresentation(BsonType.ObjectId)]
          
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? location { get; set; }

        public string? companyName { get; set; }

        public double? FuelPetrolCapacity { get; set; } = 0;

        public double? FuelDiselCapacity { get; set; } = 0;  

        public int? CarQueue { get; set; } = 0;
        public int? BikeQueue { get; set; } = 0;
        public int? BusQueue { get; set; } = 0;

        public int? ThreeWheelerQueue { get; set; } = 0;

        public string? UserEmail { get; set; }

        public string? ArrivalTime   { get; set; }




    }
}
