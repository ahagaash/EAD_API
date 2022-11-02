
using SampleApplication.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SampleApplication.Services

{
    public class MongoDBService
    {
        private readonly IMongoCollection<FuelStop> _fuelstopCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.connectionURI);
            IMongoDatabase mongoDatabase = client.GetDatabase(mongoDBSettings.Value.databaseName);
            _fuelstopCollection = mongoDatabase.GetCollection<FuelStop>(mongoDBSettings.Value.collectionName);


        }

        public async Task CreateAsycAddFuel(FuelStop fuelStop)
        {
            await _fuelstopCollection.InsertOneAsync(fuelStop);
            return;

        }

        public async Task <List<FuelStop>> GetALLFuelStopsAsync()
        {
            return await _fuelstopCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<FuelStop> GetFuelStopbyIdAsync(string id)

        {
            var data = await  _fuelstopCollection.Find<FuelStop>(fuelstop => fuelstop.Id == id).SingleAsync();
            return data;


           
           
        }

        public async Task<FuelStop> GetFuelStopbyEmailAsync(string email)

        {
            var data = await _fuelstopCollection.Find<FuelStop>(fuelstop => fuelstop.UserEmail == email).SingleAsync();
            return data;




        }

        public async Task AddToFuelQueueAsync(string id, String vechiletype)
        {
            FilterDefinition<FuelStop> filter = Builders<FuelStop>.Filter.Eq("Id", id);

            if (vechiletype.Equals("car")) { 
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("CarQueue", GetFuelStopbyIdAsync(id).Result.CarQueue+1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return ;
            } else if (vechiletype.Equals("bike"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("BikeQueue", GetFuelStopbyIdAsync(id).Result.BikeQueue + 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            } else if (vechiletype.Equals("bus"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("BusQueue", GetFuelStopbyIdAsync(id).Result.BusQueue + 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            } else if (vechiletype.Equals("threewheeler"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("ThreeWheelerQueue", GetFuelStopbyIdAsync(id).Result.ThreeWheelerQueue + 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            }


        }

        public async Task decreaseToFuelQueueAsync(string id, String vechiletype)
        {
            FilterDefinition<FuelStop> filter = Builders<FuelStop>.Filter.Eq("Id", id);

            if (vechiletype.Equals("car"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("CarQueue", GetFuelStopbyIdAsync(id).Result.CarQueue - 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            }
            else if (vechiletype.Equals("bike"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("BikeQueue", GetFuelStopbyIdAsync(id).Result.BikeQueue - 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            }
            else if (vechiletype.Equals("bus"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("BusQueue", GetFuelStopbyIdAsync(id).Result.BusQueue - 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            }
            else if (vechiletype.Equals("threewheeler"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("ThreeWheelerQueue", GetFuelStopbyIdAsync(id).Result.ThreeWheelerQueue - 1);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                return;
            }


        }


        public async Task decreaseToFuelQueueAsync(string id, String petroltype,double fuelquantity)
        {
            FilterDefinition<FuelStop> filter = Builders<FuelStop>.Filter.Eq("Id", id);

            if (petroltype.Equals("petrol"))

            {
                if ((GetFuelStopbyIdAsync(id).Result.FuelPetrolCapacity - fuelquantity) >= 0)
                {
                    UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("FuelPetrolCapacity", GetFuelStopbyIdAsync(id).Result.FuelPetrolCapacity - fuelquantity);
                    await _fuelstopCollection.UpdateOneAsync(filter, update);
                    return;
                }
            }
            else if (petroltype.Equals("diesel"))
            {
                if ((GetFuelStopbyIdAsync(id).Result.FuelDiselCapacity - fuelquantity) >= 0)
                {
                    UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("FuelDiselCapacity", GetFuelStopbyIdAsync(id).Result.FuelDiselCapacity - fuelquantity);
                    await _fuelstopCollection.UpdateOneAsync(filter, update);
                    return;
                }

            }
           


        }


        public async Task IncreaseFuelAsync(string email, String petroltype, double fuelquantity,string arrivalTime)
        {
            FilterDefinition<FuelStop> filter = Builders<FuelStop>.Filter.Eq("UserEmail", email);

            if (petroltype.Equals("petrol"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("FuelPetrolCapacity", GetFuelStopbyEmailAsync(email).Result.FuelPetrolCapacity + fuelquantity);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                UpdateDefinition<FuelStop> update1 = Builders<FuelStop>.Update.Set("ArrivalTime", arrivalTime);
                await _fuelstopCollection.UpdateOneAsync(filter, update1);
                return;
            }
            else if (petroltype.Equals("diesel"))
            {
                UpdateDefinition<FuelStop> update = Builders<FuelStop>.Update.Set("FuelDiselCapacity", GetFuelStopbyEmailAsync(email).Result.FuelDiselCapacity + fuelquantity);
                await _fuelstopCollection.UpdateOneAsync(filter, update);
                UpdateDefinition<FuelStop> update1 = Builders<FuelStop>.Update.Set("ArrivalTime",  arrivalTime);
                await _fuelstopCollection.UpdateOneAsync(filter, update1);
                return;
            }



        }
    }
}
