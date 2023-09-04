using MongoDB.Driver;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IMongoConnection
    {
        public IMongoCollection<T> ConnectToMongoDb<T>(in string collectionName);
    }
}
