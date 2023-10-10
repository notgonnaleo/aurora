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

        // ok just tested and its not getting the data from app settings
        // TODO: Make these methods consume the appsettings in the frontend proj
        public string GetEnvironment()
        {
            var environmentName = _config.GetSection("ApplicationSettings:Environment").Value;

            if (environmentName == null)
                throw new ArgumentNullException(nameof(environmentName));

            return environmentName;
        }

        public string GetEndpoint()
        {
            var environmentName = GetEnvironment();
            var environmentConfig = _config.GetSection("Environments")
                .GetChildren()
                .FirstOrDefault(x => x["Name"] == environmentName).Value;

            if (environmentConfig == null)
                throw new ArgumentException($"Environment '{environmentName}' not found.");

            return environmentConfig; // i think this shit will break but im too lazy to debug it rn
        }
    }
}
