using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using System.Drawing;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class ContactDetailsImpl : IContactDetailsService
    {
        IMongoConnection _connection = new MongoConnectionImpl();

        public Task<string> AddContactLink(ContactDto contactDto, string id)
        {

            try
            {
                var getContactDetailsModel = GetContactDetail(id);
                contactDto.ContactLinks = contactDto.ContactLinks.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                        .ToDictionary(entry => entry.Key.Trim(), entry => entry.Value.Trim());

                if (getContactDetailsModel.ContactDetailsLinks.Count > 0)
                {
                    if (getContactDetailsModel.ContactDetailsLinks == null)
                    {
                        getContactDetailsModel.ContactDetailsLinks = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }

                    getContactDetailsModel.ContactDetailsLinks = getContactDetailsModel.ContactDetailsLinks.Union(contactDto.ContactLinks)
                        .ToDictionary(entry => entry.Key, entry => entry.Value);

                }
                else
                {
                    throw new Exception("Contact cannot be null or empty.");
                }
                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);
                var filter = Builders<ContactModel>.Filter.Eq("Id", id);

                return collection.ReplaceOneAsync(filter, getContactDetailsModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Contact details updated successfully.");
            }
            catch (Exception exception)
            {
                return Task.FromResult(exception.Message);
            }

        }

        public Task<string> AddNewContactDetails(ContactDto contactDto)
        {
            try
            {
                ContactModel contactModel = new ContactModel();
                contactModel.ContactDetailsLinks = contactDto.ContactLinks;

                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);

                return collection.InsertOneAsync(contactModel).ContinueWith(_ => "Data Inserted Successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> DeleteContactDetails(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);
                var filter = Builders<ContactModel>.Filter.Eq("Id", id);

                if(collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }

                return collection.DeleteOneAsync(contdet => contdet.Id.Equals(id)).ContinueWith(_ => "Data deleted Successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContactModel>> GetAllContactDetails()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);
                var contactDetails = await collection.FindAsync(_ => true);
                return contactDetails.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ContactModel GetContactDetail(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);
                var filter = Builders<ContactModel>.Filter.Eq("Id", id);
                var getContactDetailsModel = collection.Find(filter).FirstOrDefault();

                if(getContactDetailsModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getContactDetailsModel;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message + "\nPlease enter valid Id.");
            }
        }

        public Task<string> RemoveContactLink(string contactDetailName, string id)
        {

            try
            {
                var getContactDetailsModel = GetContactDetail(id);
                if (getContactDetailsModel.ContactDetailsLinks != null)
                {
                    if (string.IsNullOrWhiteSpace(contactDetailName.ToLower().Trim()))
                    {
                        throw new Exception("Contact Detail Name canot be null");
                    }

                    if(getContactDetailsModel.ContactDetailsLinks.ContainsKey(contactDetailName.Trim()))
                    {
                        getContactDetailsModel.ContactDetailsLinks.Remove(contactDetailName.Trim());
                        if(getContactDetailsModel.ContactDetailsLinks.Count == 0)
                        {
                            getContactDetailsModel.ContactDetailsLinks = null;
                        }
                    }
                }
                else
                {
                    throw new Exception("No contact details found to remove.");
                }
                var collection = _connection.ConnectToMongoDb<ContactModel>(Constants.Constants.ContactDetailsCollection);
                var filter = Builders<ContactModel>.Filter.Eq("Id", id);

                return collection.ReplaceOneAsync(filter,getContactDetailsModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Contact details updated successfully.");
            }
            catch(Exception exception) 
            {
                return Task.FromResult(exception.Message);
            }

        }
    }
}
