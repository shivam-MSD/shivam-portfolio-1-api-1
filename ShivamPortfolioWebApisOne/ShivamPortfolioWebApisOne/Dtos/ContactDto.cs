namespace ShivamPortfolioWebApisOne.Dtos
{
    public class ContactDto
    {
        private Dictionary<string,string> _contact;
        public Dictionary<string,string> ContactLinks 
        {
            get
            {
                return _contact;
            }

            set
            {
                _contact = value.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                .ToDictionary(entry => entry.Key.Trim(), entry => entry.Value.Trim());

                if(_contact.Count == 0)
                {
                    _contact = null;
                }
            }
        }
    }
}
