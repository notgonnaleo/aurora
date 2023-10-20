using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata;
using System.Text;

namespace Frontend.Web.Models.Route
{
    public class RouteParameters
    {
        public string BuildParameterString(List<RouteParameters> routeParameters) 
        {
            string queryString = string.Empty;

            if (routeParameters == null) 
                return queryString;

            for (int i = 0; i < routeParameters.Count(); i++)
            {
                var parameter = routeParameters[i];
                queryString += $"{parameter.ParameterName}={parameter.ParameterValue}";

                if(i < routeParameters.Count - 1)
                    queryString += "&";
            }
            return queryString;
        }

        public string BuildParameterString(RouteParameters parameter)
        {
            return $"{parameter.ParameterName}={parameter.ParameterValue}";
        }

        // I'm still thinking about if I should implement a type discernation even though it will become an entire string in the end.

        public int Key { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
    }
}
