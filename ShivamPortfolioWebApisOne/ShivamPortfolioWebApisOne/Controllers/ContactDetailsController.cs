using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{

    [Route("api/contact")]
    public class ContactDetailsController : ControllerBase
    {
        IContactDetailsService contactDetailsService = new ContactDetailsImpl();

        [HttpPost]
        [Route("addcontactdetails")]
        public Task<string> AddContactDetails(ContactDto contactDto)
        {
            return contactDetailsService.AddNewContactDetails(contactDto);
        }

        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteContact(string id)
        {
            return contactDetailsService.DeleteContactDetails(id);
        }


        [HttpPut]
        [Route("addlinks")]
        public Task<string> AddContactLinks(ContactDto contactDto,string id)
        {
            return contactDetailsService.AddContactLink(contactDto, id);
        }

        [HttpPut]
        [Route("removelink")]
        public Task<string> RemoveContactLink(string contactDetailName, string id)
        {
            return contactDetailsService.RemoveContactLink(contactDetailName, id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ContactModel>> GetAllContacts()
        {
            return await contactDetailsService.GetAllContactDetails();
        }

        [HttpGet]
        [Route("")]
        public ContactModel GetContactDetail(string id)
        {
            return contactDetailsService.GetContactDetail(id);
        }

        [HttpGet]
        [Route("1")]
        public ContactModel GetFirstContactDetail()
        {
            return contactDetailsService.GetAllContactDetails().Result.FirstOrDefault();
        }
    }
}
