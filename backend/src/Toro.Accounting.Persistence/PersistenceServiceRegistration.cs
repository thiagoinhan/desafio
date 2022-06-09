using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Domain.Entities;
using Toro.Accounting.Persistence.Repositories;
using Toro.Accounting.Persistence.Settings;

namespace Toro.Accounting.Infrastructure
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            services.AddScoped<ICustomerRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new CustomerRepository(database);
            });

            return services;
        }

        public static void SeedData(this IMongoDatabase database)
        {
            var collection = database.GetCollection<Customer>(Customer.TableName);
            var totalCustomers = collection.CountDocuments(Builders<Customer>.Filter.Empty);

            if (totalCustomers == 0)
            {
                var customers = new List<Customer>
                {
                    new Customer(Guid.Parse("9623f6d8-01d7-420f-9264-2143df3557cd"), "luffy", "Monkey D. Luffy", "87498578964", "283749", 2000),
                    new Customer(Guid.Parse("56fe0147-97d1-4b24-87c1-00ff67c40f8f"), "tanjiro", "Tanjiro Kamado", "74894803977", "839456", 0),
                    new Customer(Guid.Parse("bef882d7-9359-44ec-9f4b-77c71413592c"), "eren", "Eren Yeager", "17384975822", "139029", 1000),
                };

                foreach (var customer in customers)
                {
                    collection.InsertOne(customer);
                }
            }
        }
    }
}
