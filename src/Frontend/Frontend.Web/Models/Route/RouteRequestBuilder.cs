using Backend.Infrastructure.Enums.Modules;

namespace Frontend.Web.Models.Route
{
    public class RouteBuilder<T>
    {
        /// <summary>
        /// Send the request parameter or object with the selected module, building the Route model for the request.
        /// </summary>
        /// <param name="moduleEndpoint">Service module being accessed</param>
        /// <param name="moduleMethod">Http REST method being executed</param>
        /// <param name="body">Object model</param>
        /// <returns>Type of RouteBuilder<T> using the provided generic type.</returns>
        public RouteBuilder<T> Send(string moduleEndpoint, string moduleMethod, T body)
        {
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint, moduleMethod, body);
        }

        /// <summary>
        /// Send the request parameter or object with the selected module, parameterless.
        /// </summary>
        /// <param name="moduleEndpoint">Service module being accessed</param>
        /// <param name="moduleMethod">Http REST method being executed</param>
        /// <returns>Type of RouteBuilder<T> using the provided generic type.</returns>
        public RouteBuilder<T> Send(string moduleEndpoint, string moduleMethod)
        {
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint, moduleMethod);
        }

        /// <summary>
        /// Send the request parameter or object with the selected module, parameterless.
        /// </summary>
        /// <param name="moduleEndpoint">Service module being accessed</param>
        /// <param name="moduleMethod">Http REST method being executed</param>
        /// <returns>Type of RouteBuilder<T> using the provided generic type.</returns>
        public RouteBuilder<T> Send(string moduleEndpoint, string moduleMethod, List<RouteParameterRequest> parameters)
        {
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint, moduleMethod);
        }

        /// <summary>
        /// Send the request parameter or object with the selected module, building the Route model for the request.
        /// </summary>
        /// <param name="moduleEndpoint">Service module being accessed</param>
        /// <param name="moduleMethod">Http REST method being executed</param>
        /// <param name="parameter">One parameter only</param>
        /// <param name="body">Object model</param>
        /// <returns>Type of RouteBuilder<T> using the provided generic type.</returns>
        public RouteBuilder<T> Send(string moduleEndpoint, string moduleMethod, RouteParameterRequest parameter)
        {
            return new RouteBuilder<T>()
                .BuildRoute(moduleEndpoint,
                moduleMethod,
                new RouteParameters().BuildParameterString(new RouteParameters()
                {
                    ParameterName = parameter.ParameterName,
                    ParameterValue = parameter.ParameterValue,
                })!
                );
        }

        /// <summary>
        /// Send the request a list of parameters or object with the selected module, building the Route model for the request.
        /// </summary>
        /// <param name="moduleEndpoint">Service module being accessed</param>
        /// <param name="moduleMethod">Http REST method being executed</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="body">Object model</param>
        /// <returns>Type of RouteBuilder<T> using the provided generic type.</returns>
        public RouteBuilder<T> SendMultiple(string moduleEndpoint, string moduleMethod, List<RouteParameterRequest> parameters)
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
                new RouteParameters().BuildParameterString(parameterRequestList));
        }

        /// <summary>
        /// Build the route after receiving Send methods response.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="actionName"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public RouteBuilder<T> BuildRoute(string endpoint, string actionName, T body)
        {
            RouteBuilder<T> route = new RouteBuilder<T>()
            {
                Endpoint = endpoint,
                ActionName = actionName,
                Body = body
            };
            return route;
        }

        /// <summary>
        /// Build the route after receiving Send methods response.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="actionName"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public RouteBuilder<T> BuildRoute(string endpoint, string actionName, string parameters)
        {
            RouteBuilder<T> route = new RouteBuilder<T>()
            {
                Endpoint = endpoint,
                ActionName = actionName,
                Parameters = parameters
            };
            return route;
        }

        /// <summary>
        /// Build the route after receiving Send methods response.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="actionName"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public RouteBuilder<T> BuildRoute(string endpoint, string actionName)
        {
            RouteBuilder<T> route = new RouteBuilder<T>()
            {
                Endpoint = endpoint,
                ActionName = actionName,
            };
            return route;
        }

        /// <summary>
        /// Route Request Builder Properties
        /// </summary>
        public string Endpoint { get; set; }
        public string ActionName { get; set; }
        public string? Parameters { get; set; }
        public T? Body { get; set; }
    }
}