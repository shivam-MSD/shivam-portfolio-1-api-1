namespace ShivamPortfolioWebApisOne.Constants
{
    public class Constants
    {
        public static readonly HashSet<string> supportedImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    ".jpg",
                    ".jpeg",
                    ".png",
                    ".gif"
                };


        public static readonly string HeaderCollection = "headercollection";
        public static readonly string HomeCollection = "homecollection";
        public static readonly string AboutCollection = "aboutcollection";
        public static readonly string ContactDetailsCollection = "contactdetailscollection";
        public static readonly string EducationCollection = "educationcollection";
        public static readonly string WorkExperienceCollection = "workexperiencecollection";
        public static readonly string ProjectCollection = "projectcollection";
        public static readonly string ArticleCollection = "articlecollection";
        public static readonly string ResumeCollection = "resumecollection";

        private static IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

        public static readonly string connectionString = configuration.GetConnectionString("MongoDB");
        public static readonly string databaseName = configuration.GetValue<string>("DatabaseName");


        //public static readonly string connectionString = "mongodb://localhost:27017";
        //public static readonly string databaseName = "UserDb";
    }
}
