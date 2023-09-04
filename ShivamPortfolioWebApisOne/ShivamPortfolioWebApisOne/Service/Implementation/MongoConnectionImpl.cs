using MongoDB.Driver;
using System;
using System.Configuration;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class MongoConnectionImpl : IMongoConnection
    {
        //private string connectionString = "mongodb://localhost:27017";
        //private string databaseName = "Shivamtest";

        private string connectionString = Constants.Constants.connectionString;
        private string databaseName = Constants.Constants.databaseName;
        //private string connectionString = ConfigurationManager.AppSettings["mongodbconnectionstring"];
        //private string databaseName = ConfigurationManager.AppSettings["databasename"];
        public IMongoCollection<T> ConnectToMongoDb<T>(in string collectionName)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                return database.GetCollection<T>(collectionName);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
