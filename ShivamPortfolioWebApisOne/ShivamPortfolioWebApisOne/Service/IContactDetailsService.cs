using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IContactDetailsService
    {
        public Task<string> AddNewContactDetails(ContactDto contactDto);

        public Task<string> DeleteContactDetails(string id);

        public Task<string> AddContactLink(ContactDto contactDto, string id);

        public Task<string> RemoveContactLink(string contactDetailName,string id);

        public Task<List<ContactModel>> GetAllContactDetails();

        public ContactModel GetContactDetail(string id);

    }
}
