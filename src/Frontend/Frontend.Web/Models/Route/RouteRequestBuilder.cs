using Backend.Infrastructure.Enums.Modules;

namespace Frontend.Web.Models.Route
{
    public class RouteBuilder<T>
    {
        public RouteBuilder<T> Send(string moduleEndpoint, string moduleMethod, RouteParameterRequest? parameters, T? body)
        {
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint,
                moduleMethod,
                new RouteParameters().BuildParameterString(new RouteParameters()
                {
                    Key = 1,
                    ParameterName = parameters.ParameterName,
                    ParameterValue = parameters.ParameterValue,
                }),
                body);
        }

        public RouteBuilder<T> SendMultiple(string moduleEndpoint, string moduleMethod, List<RouteParameterRequest> parameters, T? body)
        {
            int key = 0;
            var parameterRequestList = new List<RouteParameters>();

            foreach (var parameter in parameters)
            {
                parameterRequestList.Add(new RouteParameters()
                {
                    Key = key,
                    ParameterName = parameter.ParameterName,
                    ParameterValue = parameter.ParameterValue
                });
                key++;
            }
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint,
                moduleMethod,
                new RouteParameters().BuildParameterString(parameterRequestList),
                body);
        }
        public RouteBuilder<T> BuildRoute(string endpoint, string actionName, string? parameters, T? body)
        {
            RouteBuilder<T> route = new RouteBuilder<T>()
            {
                Endpoint = endpoint,
                ActionName = actionName,
                Parameters = parameters,
                Body = body
            };
            return route;
        }
        // i should make the parameters value be an object maybe?

        /// <summary>
        /// Route Request Builder Properties
        /// </summary>
        public string Endpoint { get; set; }
        public string ActionName { get; set; }
        public string? Parameters { get; set; }
        public T? Body { get; set; }
    }
}