using Backend.Infrastructure.Enums.Modules;

namespace Frontend.Web.Models.Route
{
    public class RouteBuilder<T>
    {
        public RouteBuilder<T> BuildRoute(string endpoint, string actionName, string parameters, T? model)
        {
            RouteBuilder<T> route = new RouteBuilder<T>()
            {
                Endpoint = endpoint,
                ActionName = actionName,
                Parameters = parameters,
                Model = model
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
        public T? Model { get; set; }
    }
}
