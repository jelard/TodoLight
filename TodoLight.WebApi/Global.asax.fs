namespace TodoLight.WebApi

open System
open System.Net.Http
open System.Web
open System.Web.Http
open System.Web.Routing
open System.Web.Http.Dispatcher
open System.Web.Http.Controllers
open TodoLight.Controllers
open TodoLight.DataAccess

type HttpRoute = {
    controller : string
    id : RouteParameter }

type CompositionRoot() =
    interface IHttpControllerActivator with
        member this.Create(request, controllerDescriptor, controllerType) =
            if controllerType = typeof<TodoController> then
                new TodoController(TodoRepository()) :> IHttpController
            else
                 invalidArg (sprintf "Unknown controller type requested: %O" controllerType) "controllerType"


type Global() =
    inherit System.Web.HttpApplication() 

    static member RegisterWebApi(config: HttpConfiguration) =
        // Configure routing
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            "DefaultApi", // Route name
            "api/{controller}/{id}", // URL with parameters
            { controller = "{controller}"; id = RouteParameter.Optional } // Parameter defaults
        ) |> ignore

        // Configure serialization
        config.Formatters.XmlFormatter.UseXmlSerializer <- true
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        config.Services.Replace(
            typeof<IHttpControllerActivator>,CompositionRoot())

        // Additional Web API settings

    member x.Application_Start() =
        GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
