using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IHeaderService
    {
        /// <summary>
        /// This method is used to add the header Logo
        /// </summary>
        /// <param name="headerDto"></param>
        /// <returns></returns>
        public Task<string> AddHeaderLogo(HeaderDto headerDto);

        /// <summary>
        /// This method is used to edit the Logo present in the header or nav bar
        /// </summary>
        /// <param name="headerDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> UpdateHeaderLogo(HeaderDto headerDto,string id);

        /// <summary>
        /// This method is used to delete log detail present in the header.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> DeleteHeaderLogo(string id);

        /// <summary>
        /// THis methos is used to get the specific header
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HeaderModel GetHeader(string id);

        /// <summary>
        /// This method is used to display logo on the header section according to the highest priority
        /// </summary>
        /// <returns></returns>
        public HeaderModel DisplayLogo();

        /// <summary>
        /// This method is used to get all the header list
        /// </summary>
        /// <returns></returns>
        public Task<List<HeaderModel>> GetAllHeader();
    }
}
