using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace Frontend.Web.Util.Environments
{
    public class EnvironmentHandler
    {
        private readonly IConfiguration _config;
        public EnvironmentHandler(IConfiguration config)
        {
            _config = config;
        }

        public string GetEnvironment()
        {
            var environmentName = _config.GetSection("ApplicationSettings")["Environment"].ToString();

            if (environmentName == null)
                throw new ArgumentNullException(nameof(environmentName));

            return environmentName;
        }

        public string GetEndpoint()
        {
            // Suggestion: Maybe create an object for this information
            // TODO: Create an ENUM for QA, Local and Prod flags.
            var environmentName = GetEnvironment();
            var environmentConfig = _config.GetSection("Environments")
                                       .GetChildren()
                                       .FirstOrDefault(x => x["Name"] == environmentName)
                                       ?["Endpoint"];

            if (environmentConfig == null)
                throw new ArgumentException($"Environment '{environmentName}' not found.");

            return environmentConfig;
        }
    }
}
